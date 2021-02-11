#!/bin/sh

# Example output from `gphoto2 --auto-detect`:
#	Model                          Port
#	----------------------------------------------------------
#	Nikon DSC D810                 usb:002,002

# Expected output from script
#	Nikon DSC D810===usb:002,002


# Remove the line containing Model and Port
# Remove the line containing a long line
# Separates the model name from the port by '==='
gphoto2 --auto-detect\
|	grep -i -v "Model"\
|	grep -v -E "\-+"\
|	sed -r -n 's/(.+)(usb:.+)/\1===\2/p'
