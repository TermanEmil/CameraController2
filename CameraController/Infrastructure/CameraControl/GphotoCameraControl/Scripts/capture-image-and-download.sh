#!/bin/bash

# Parameters
#       $1: filename
#       $2: port

# Example output from gphoto2 command given filename "Foo":
#       New file is in location /capt0000.nef on the camera
#       Saving file as Foo.nef
#       Deleting file /capt0000.nef on the camera

# Expected output from script
#       Foo.nef

# gphoto2:
#       --filename %C: file sufix

# sed:
#       -n: no print (skip likes that don't match')
#       p: print what matched
#       I: case insensitive

# Capture image on the given port and download it with the given name.
# Filter out only the final filename (whatever follows 'Saving file as ')
gphoto2 --capture-image-and-download --filename "$1.%C" --port "$2" --force-overwrite\
|       sed -n 's/Saving file as //Ip'

