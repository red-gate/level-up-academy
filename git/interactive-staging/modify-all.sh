#! /bin/bash

# Modify all the 'source' files
for file in $(find . -name '*.js')
do
  echo "console.log('a trivial change');" >> $file;
done
