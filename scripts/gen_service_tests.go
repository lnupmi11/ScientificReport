package main

import (
	"fmt"
	"io/ioutil"
	"os"
	"strings"
)

const (
	TEMPLATE_DIR = "./templates/"

	SERVICE_TEST_TEMPLATE = TEMPLATE_DIR + "service_test.tmpl"

	OUT = "../ScientificReport.Test/"

	SERVICES_TESTS_OUT = OUT + "ServicesTests/"
)


func read(path string) (string, error) {
	bytesData, err := ioutil.ReadFile(path)
	return string(bytesData), err
}

func write(path string, data string) error {
	f, err := os.Create(path)
	defer f.Close()
	if err != nil {
		fmt.Println(err)
		return err
	}
	_, err = f.WriteString(data)
	if err != nil {
		fmt.Println(err)
		return err
	}
	return nil
}

func render(entity string, template string) string {
	res := strings.Replace(template, "{{ entity }}", entity, -1)
	res = strings.Replace(res, "{{ entity_var }}", string(strings.ToLower(string(entity[0]))) + entity[1:], -1)
	return res
}

func generate(entityName string) {
	testTemplate, err := read(SERVICE_TEST_TEMPLATE)
	if err != nil {
		fmt.Println(entityName + "ServiceTests: " + err.Error())
		return
	}

	testPath := SERVICES_TESTS_OUT + entityName + "ServiceTests.cs"
	if _, err := os.Stat(testPath); os.IsNotExist(err) {
		err = write(testPath, render(entityName, testTemplate))
		if err != nil {
			fmt.Println(entityName + "ServiceTests: " + err.Error())
			return
		}
		fmt.Println("'" + testPath + "' generated")
	}
}

func main() {
	entities := os.Args[1:]
	if len(entities) == 0 {
		fmt.Println("Usage: go run gen_service_tests.go entity1 entity2 ...")
	} else {
		for _, entity := range entities {
			generate(entity)
		}
	}
}
