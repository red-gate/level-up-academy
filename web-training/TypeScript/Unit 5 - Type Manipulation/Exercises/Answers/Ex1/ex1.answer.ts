import { Pet, Dog, Cat } from "./pets.answer";

class TreeNode<TValue> {
    value: TValue;
    children: TreeNode<TValue>[];

    constructor (value: TValue, children?: TreeNode<TValue>[]) {
        this.value = value;
        this.children = children ? Array.from(children) : [];
    }
}

function getLongestString(a: string, b: string) : string {
    return a.length > b.length ? a : b;
}

interface HasLength {
    length: number;
}

function getLongestOld(a: HasLength, b: HasLength) : HasLength {
    return a.length > b.length ? a : b;
}

function getLongest<T extends HasLength>(a: T, b: T) : T {
    return a.length > b.length ? a : b;
}

function reduce<TValue, TResult>(tree: TreeNode<TValue>, reducer: (aggregate: TResult, value: TValue) => TResult, initialValue: TResult): TResult {    
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

const arrayTree = new TreeNode([1, 2, 3], [
    new TreeNode([1, 2, 3, 4]),
    new TreeNode([1, 2, 3], [
        new TreeNode([1, 2])
    ])
])

const petOrgChart = new TreeNode(new Cat("Fluffy", "Mark", 8), [
    new TreeNode(new Dog("Lexi", "Piers", false)),
    new TreeNode<Pet>(new Dog("Fido", "Alex", true), [
        new TreeNode(new Cat("Simba", "Sami", 9))
    ])
])

function getLongestName(currentLongest: Pet, value: Pet) : Pet {
    return currentLongest.name.length > value.name.length ? currentLongest : value;
}

function getLongestField<Key extends keyof Pet>(key: Key) {
    return function(currentLongest: Pet, value: Pet) : Pet {
        return currentLongest[key].length > value[key].length ? currentLongest : value;
    }
}

console.log(reduce(nameTree, getLongestString, ""));
console.log(reduce<number[], number[]>(arrayTree, getLongest, []));
console.log(reduce<Pet, Pet>(petOrgChart, getLongestName, petOrgChart.value));
console.log(reduce<Pet, Pet>(petOrgChart, getLongestField("owner"), petOrgChart.value));
