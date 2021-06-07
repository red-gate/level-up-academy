// Create a FeatureFlags type by applying Options<T> to the Features type.

type Features = {
    enableDarkMode: () => void;
    enableNewUserProfile: () => void;
    enableNewOnboarding: () => void;
}

type Options<T> = {
    [key in keyof T]: boolean;
}

type FeatureFlags = Options<Features>;

// ------------------------------------------------------------------------

// Create an readonly version of UserAccount by applying Immutable<T> to it.

type UserAccount = {
    id: string;
    name?: string;
    enabledFrom: Date;
    enabledUntil?: Date;
}

type Immutable<T> = {
    +readonly [Property in keyof T]: T[Property];
}

type ReadOnlyAccount = Immutable<UserAccount>;

// ------------------------------------------------------------------------

// Create a type where no property is optional by applying Concrete<T> to UserAccount.

type Concrete<T> = {
    [prop in keyof T]-?: T[prop];
}

type ConcreteUserAccount = Concrete<UserAccount>;

// ------------------------------------------------------------------------

// Create a lazilly-evaluated version of UserAccount by applying Lazy<T> to it.

type Lazy<T> = {
    [P in keyof T as `get${Capitalize<string & P>}`]: () => T[P]; 
}

type LazyUserAccount = Lazy<UserAccount>;

// ------------------------------------------------------------------------

// Create a new type where the id property is excluded, by applying ExcludeId<T> to Account.

type ExcludeId<T> = {
    [Property in keyof T as Exclude<Property, "id">]: T[Property];
}

type UserAccountWithoutId = ExcludeId<UserAccount>;

// Create a new type where you apply, Lazy, Concrete and ExcludeId to UserAccount

type AllTheThings = Lazy<Concrete<ExcludeId<UserAccount>>>;

// ------------------------------------------------------------------------