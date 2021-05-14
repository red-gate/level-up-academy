class TreeNode {
    value: number;
    children: TreeNode[];

    constructor (value: number, children?: TreeNode[]) {
        this.value = value;
        this.children = children ? Array.from(children) : [];
    }
}

function sum(a: number, b: number) : number {
    return a + b;
}

function reduce(tree: TreeNode, reducer: (aggregate: number, value: number) => number, initialValue: number): number {    
    let result = reducer(initialValue, tree.value);

    tree.children.forEach(child => {
        result = reduce(child, reducer, result);
    });

    return result;
}

const tree = new TreeNode(1, [
    new TreeNode(2),
    new TreeNode(3, [
        new TreeNode(4),
        new TreeNode(5)
    ])
])

console.log(reduce(tree, sum, 0));
console.log(reduce(tree, (result, tree) => result = Math.max(result, tree.value), 0));
