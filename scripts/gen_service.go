package main

import (
	"fmt"
	"io/ioutil"
	"os"
	"strings"
)

const (
	TEMPLATE_DIR = "./templates/"

	INTERFACE_TEMPLATE = TEMPLATE_DIR + "interface.tmpl"
	SERVICE_TEMPLATE = TEMPLATE_DIR + "service.tmpl"

	OUT = "../ScientificReport.BLL/"

	INTERFACE_OUT = OUT + "Interfaces/"
	SERVICE_OUT = OUT + "Services/"
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
	res = strings.Replace(res, "{{ entity_name }}", string(strings.ToLower(string(entity[0]))) + entity[1:], -1)
	return res
}

func generate(entityName string) {
	interfaceTemplate, err := read(INTERFACE_TEMPLATE)
	if err != nil {
		fmt.Println(entityName + ": " + err.Error())
		return
	}
	serviceTemplate, err := read(SERVICE_TEMPLATE)
	if err != nil {
		fmt.Println(entityName + ": " + err.Error())
		return
	}

	interfacePath := INTERFACE_OUT + "I" + entityName + "Service.cs"
	if _, err := os.Stat(interfacePath); os.IsNotExist(err) {
		err = write(interfacePath, render(entityName, interfaceTemplate))
		if err != nil {
			fmt.Println(entityName + ": " + err.Error())
			return
		}
		fmt.Println("'" + interfacePath + "' generated")
	}

	servicePath := SERVICE_OUT + entityName + "Service.cs"
	if _, err := os.Stat(servicePath); os.IsNotExist(err) {
		err = write(servicePath, render(entityName, serviceTemplate))
		if err != nil {
			fmt.Println(entityName + ": " + err.Error())
			return
		}
		fmt.Println("'" + servicePath + "' generated")
	}
}


func main() {
	entities := os.Args[1:]
	if len(entities) == 0 {
		fmt.Println("Usage: go run gen_service.go entity1 entity2 ...")
	} else {
		for _, entityName := range entities {
			generate(entityName)
		}
	}
}
