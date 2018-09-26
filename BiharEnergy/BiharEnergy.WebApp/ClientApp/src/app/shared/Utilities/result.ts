export class Result
{
    protected _isSuccess : boolean;    
    get IsSuccess() {
    return this._isSuccess;
    }

    get isFailure() {
    return !this._isSuccess;
    }

    private _error : string;    
    get error() {
    return this._error;
    }


 public constructor(isSuccess:boolean, error:string)
    {
        if (isSuccess && error != null)
            throw new Error("Invalid Operation");
        if (!isSuccess && error == null)
              throw new Error("Invalid Operation");

        this._isSuccess = isSuccess;
        this._error = error;
    }

    public static Fail(message:string) : Result
    {
        return new Result(false, message);
    }
    public static Ok() : Result
    {
        return new Result(true, null);
    }

    // public static Fail<T=any>(message:string) :Result<T>
    // {
    //     return new Result<T>(null, false, message);
    // }


    // public static Ok<T>(value:T) : Result<T>
    // {
    //     return new Result<T>(value, true, null);
    // }
}

// export class Result<T=void> extends Result
// {
//     private readonly _value:T;
//     get Value() : T
//     {
//             if (!this._isSuccess)
//                 throw new Error("Invalid Operation");

//             return this._value;
//     }

//  constructor(value:T,isSuccess:boolean, error :string)
//     {
//         super(isSuccess,error)
//         this._value = value;
//     }
// }
