class TreeNode {
    value: number;
    children: TreeNode[];

    constructor (value: number, children?: TreeNode[]) {
        this.value = value;
        this.children = children ? Array.from(children) : [];
    }
}

function getSum(tree: TreeNode): number {
    let result = tree.value;

    tree.children.forEach(child => {
        result += getSum(child);
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

console.log(getSum(tree));

