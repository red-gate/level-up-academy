#! /bin/bash

# Modify all the 'source' files
for file in $(find . -name '*.js')
do
  echo "console.log('a trivial change');" >> $file;
done

# Add some new files
echo "console.log('tangerines');" > dir1/tangerines.js
echo "console.log('blueberries');" > dir2/blueberries.js
