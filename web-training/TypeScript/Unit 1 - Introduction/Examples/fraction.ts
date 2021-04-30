export interface IMathApi {
    divide(num: number, den: number): number;
} 

export class Fraction {
    public readonly numerator: number;
    public readonly denominator: number;
    private readonly mathApi: IMathApi;

    public constructor(num: number, den: number, mathApi: IMathApi) {
        this.numerator = num;
        this.denominator = den;
        this.mathApi = mathApi;
    }

    public toNumber(onError: (_: string) => void): number {
        try
        {
            return this.mathApi.divide(this.numerator, this.denominator);
        }
        catch (error)
        {
            if (onError) {
                onError(`Error when fetching ${this.numerator}/${ this.denominator}: ${error}`);
            }
            return Number.NaN;
        }
    }
    
    public toString() : string {
        return `${this.numerator}/${this.denominator}`;
    }
}
