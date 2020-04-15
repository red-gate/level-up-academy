#! /bin/bash

touch index.js
git init .
git add .

git commit -m "Initial commit"
git branch topic-branch
git checkout topic-branch

cat << END > index.js
function hello_world {
  console.log("Hello wrld")
}
END
git commit -am "Add new function"

cat << END > index.js
function hello_world {
  console.log("Hello world")
}
END
git commit -am "Fix typo"

cat << END > index.js
function hello_world() {
  console.log("Hello world")
}
END
git commit -am "Fix syntax error"

cat << END > index.js
function hello_world() {
  console.log("Hello world");
}
END
git commit -am "Add missing semicolon"
