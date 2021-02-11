#!/bin/sh

# Parameters
#       $1: port

# The script outputs a jpeg image to stdout

gphoto2 --capture-preview --port "$1" --stdout
