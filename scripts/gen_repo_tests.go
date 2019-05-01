package main

import (
	"fmt"
	"io/ioutil"
	"os"
	"strings"
)

const (
	TEMPLATE_DIR = "./templates/"

	REPO_TEST_TEMPLATE = TEMPLATE_DIR + "repo_test.tmpl"

	OUT = "../ScientificReport.Test/"

	REPO_TESTS_OUT = OUT + "RepositoriesTests/"
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
	repoTestTemplate, err := read(REPO_TEST_TEMPLATE)
	if err != nil {
		fmt.Println(entityName + "RepositoryTests: " + err.Error())
		return
	}

	repoTestPath := REPO_TESTS_OUT + entityName + "RepositoryTests.cs"
	if _, err := os.Stat(repoTestPath); os.IsNotExist(err) {
		err = write(repoTestPath, render(entityName, repoTestTemplate))
		if err != nil {
			fmt.Println(entityName + "RepositoryTests: " + err.Error())
			return
		}
		fmt.Println("'" + repoTestPath + "' generated")
	}
}

func main() {
	repos := os.Args[1:]
	if len(repos) == 0 {
		fmt.Println("Usage: go run gen_repo_tests.go entity1 entity2 ...")
	} else {
		for _, repo := range repos {
			generate(repo)
		}
	}
}
