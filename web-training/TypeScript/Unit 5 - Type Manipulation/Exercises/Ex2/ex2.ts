type MyArrayType = {
    [key: number]: unknown;
}
const mat: MyArrayType = [];

mat[1] = true;
mat[2] = "false";

console.log('MyArrayType is an', typeof mat, 'containing', mat);

type MyArrayIndex = keyof MyArrayType;
let first: MyArrayIndex = 0;
mat[first] = "zero-based index";

console.log('MyArrayType now contains', mat);


type MyBooleanMapType = {
    [key: string]: boolean
};
type MyBooleanMapKey = keyof MyBooleanMapType;
const map: MyBooleanMapType= {
    [1]: true,
    ["two"]: false
}
console.log('MyBooleanMapType is an', typeof map, 'containing', map);

type MyGeoPosition = { latitude: number; longitude: number };
type MyObjectKeys = keyof MyGeoPosition;

const xPosition: MyObjectKeys = "longitude";
const yPosition: MyObjectKeys = "latitude";
const mo: MyGeoPosition = {
    latitude: 52.205338,
    longitude: 0.121817
};
mo[xPosition] += 3;
mo[yPosition] += 5;

console.log('MyGeoPosition is an', typeof mo, 'containing', mo);