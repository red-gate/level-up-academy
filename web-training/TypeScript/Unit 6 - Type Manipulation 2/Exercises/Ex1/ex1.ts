
type Features = {
    enableDarkMode: () => void;
    enableNewUserProfile: () => void;
    enableNewOnboarding: () => void;
}

type Options<T> = {
    [key in keyof T]: boolean;
}

// TODO: Create a FeatureFlags type by applying Options<T> to the Features type.

type UserAccount = {
    id: string;
    name?: string;
    enabledFrom: Date;
    enabledUntil?: Date;
}

type Immutable<T> = {
    +readonly [Property in keyof T]: T[Property];
}

// TODO: Create an readonly version of UserAccount by applying Immutable<T> to it.

type Concrete<T> = {
    [prop in keyof T]-?: T[prop];
}

// TODO: Create a type where no property is optional by applying Concrete<T> to UserAccount.


type Lazy<T> = {
    [P in keyof T as `get${Capitalize<string & P>}`]: () => T[P]; 
}

// TODO: Create a lazilly-evaluated version of UserAccount by applying Lazy<T> to it.

type ExcludeId<T> = {
    [Property in keyof T as Exclude<Property, "id">]: T[Property];
}

// TODO: Create a new type where the id property is excluded, by applying ExcludeId<T> to Account.

// TODO: Create a new type where you apply Lazy, Concrete and ExcludeId to UserAccount


