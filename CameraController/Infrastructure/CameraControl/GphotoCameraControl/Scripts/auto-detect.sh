#!/bin/bash

# Example output:
#   Nikon DSC D810     ===usb:003,003 

# On osx, `sed` has a different flag for regex
if [ "$(uname -s)" = "Darwin" ]
then
	sed_regex_flag='-E';
else
	sed_regex_flag='-r';
fi


# Remove the line containing Model and Port
# Remove the line containing a long line
# Separates the model name from the port by '==='
gphoto2 --auto-detect\
|	grep -i -v "Model"\
|	grep -v -E "\-+"\
|	sed ${sed_regex_flag} -n 's/(.+)(usb:.+)/\1===\2/p'

