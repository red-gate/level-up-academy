#! /bin/bash

# Create files
mkdir -p dir1
mkdir -p dir2
mkdir -p somewhere/nested/dir3

echo "console.log('apples');" > dir1/apples.js
echo "console.log('bananas');" > dir1/bananas.js

echo "console.log('grapes');" > dir2/grapes.js
echo "console.log('kiwis');" > dir2/kiwis.js
echo "console.log('lemons');" > dir2/lemons.js

echo "console.log('oranges');" > somewhere/nested/dir3/oranges.js
echo "console.log('strawberries');" > somewhere/nested/dir3/strawberries.js

# Create a git repo
git init .

# Add the files
git add .

# Make an initial commit
git commit -m "Initial commit"
