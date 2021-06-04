import { Pet } from "./pets";

class TreeNode {
    value: string;
    children: TreeNode[];

    constructor (value: string, children?: TreeNode[]) {
        this.value = value;
        this.children = children ? Array.from(children) : [];
    }
}

function getLongestString(a: string, b: string) : string {
    return a.length > b.length ? a : b;
}

function getLongestName(currentLongest: Pet, value: Pet) : Pet {
    return currentLongest.name.length > value.name.length ? currentLongest : value;
}

interface HasLength {
    length: number;
}

function getLongest(a: HasLength, b: HasLength) : HasLength {
    return a.length > b.length ? a : b;
}

function reduce(tree: TreeNode, reducer: (aggregate: string, value: string) => string, initialValue: string): string {    
    let result = reducer(initialValue, tree.value);

    tree.children.forEach(child => {
        result = reduce(child, reducer, result);
    });

    return result;
}

const nameTree = new TreeNode("Fluffy", [
    new TreeNode("Lexi"),
    new TreeNode("Fido", [
        new TreeNode("Simba")
    ])
])

// const arrayTree = new TreeNode([1, 2, 3], [
//     new TreeNode([1, 2, 3, 4]),
//     new TreeNode([1, 2, 3], [
//         new TreeNode([1, 2])
//     ])
// ])

// const petOrgChart = new TreeNode(new Cat("Fluffy", "Mark", 8), [
//     new TreeNode(new Dog("Lexi", "Piers", false)),
//     new TreeNode(new Dog("Fido", "Alex", true), [
//         new TreeNode(new Cat("Simba", "Sami", 9))
//     ])
// ])

console.log(reduce(nameTree, getLongestString, ""));
