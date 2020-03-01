#!/bin/bash 

# Example output:
#   Nikon DSC D810|usb:003,003 

# On osx, `sed` has a different flag for regex
if [ "$(uname -s)" = "Darwin" ]
then
	sed_regex_flag='-E';
else
	sed_regex_flag='-r';
fi

gphoto2 --auto-detect\
	| grep -i -v "Model"\					# Remove the line containing Model and Port
	| grep -v -E "\-+"\						# Remove the line containing a long line
	| sed ${sed_regex_flag} 's/   +/|/'		# Replace the first 3+ spaces with a pipe
