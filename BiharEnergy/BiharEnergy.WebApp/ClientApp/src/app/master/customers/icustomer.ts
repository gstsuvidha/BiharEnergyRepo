export interface ICustomer {
    id: number,
    // customerOpeningDate: Date,
    name: string,
    gstin: string,
    address: string,
    state: string,
    contactNumber: number,
    registrationType: RegistrationType,
    email : string;
    // openBalance: number,
    
}
export enum RegistrationType {
    Registered,
    Unregistered,
    CompositeDealer
}
