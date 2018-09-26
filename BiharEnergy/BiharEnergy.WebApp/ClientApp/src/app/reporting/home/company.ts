export interface Company {
    id : number;
    name : string;
    
}
export interface IKeyValuePairInteger{
    value:number;
    label:string;
}
export const CompanyList:IKeyValuePairInteger[]=[
    {value : 1, label : "BSPHCL"},
    {value : 2, label : "SBPDCL"},
    {value : 3, label : "NBPDCL"},
    {value : 4, label : "BSPTCL"},
    {value : 5, label : "BSPGCL"},
    {value : 6, label : "TRIAL"}
]