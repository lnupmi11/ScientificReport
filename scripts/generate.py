#!/usr/bin/env python
import jinja2
import os
from sys import argv

FILENAME = argv[0]
SCRIPTS_DIR = os.path.dirname(os.path.abspath(FILENAME))
BASE_DIR = os.path.dirname(SCRIPTS_DIR)
REPOSITORY_DIR = os.path.join(BASE_DIR, 'ScientificReport.DAL/Repositories')
REPOSITORY_TEMPLATE_FILE = "repository.cs.j2"
ENTITIES_DIR = os.path.join(BASE_DIR, 'ScientificReport.DAL/Entities')

template_loader = jinja2.FileSystemLoader(searchpath=SCRIPTS_DIR)
template_env = jinja2.Environment(loader=template_loader)
repository_template = template_env.get_template(REPOSITORY_TEMPLATE_FILE)


def repository(name):
    filename = os.path.join(REPOSITORY_DIR, name + 'Repository.cs')
    if os.path.isfile(filename):
        print("Delete or rename the file {} before generating a new one".format(filename))
        exit(1)
    name_singular = name[:len(name) - 1] if name[len(name) - 1] == 's' else name
    output_text = repository_template.render(entity=name, entity_singular=name_singular)
    return filename, output_text


if __name__ == '__main__':
    if len(argv) != 3:
        print("Usage: {} <class_type> <class_name>".format(FILENAME))
        exit(1)
    class_type = argv[1]
    class_name = argv[2]

    entity_file = os.path.join(ENTITIES_DIR, class_name + '.cs')
    if not os.path.isfile(entity_file):
        print("Class {} does not exist, or is not at path {}".format(class_name, entity_file))
        exit(1)

    if class_type == 'repository':
        filename, output_text = repository(class_name)
        with open(filename, 'w+') as f:
            f.write(output_text)
