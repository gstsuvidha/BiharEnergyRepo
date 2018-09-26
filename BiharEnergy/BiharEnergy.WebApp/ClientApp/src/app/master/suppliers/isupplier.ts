export interface ISupplier {
    id: number,
    supplierOpeningDate: Date,
    name: string,
    gstin: string,
    address: string,
    state: string,
    contactNumber: string,
    openBalance: number,
    registrationType: RegistrationType,
    email : string;
}

export enum RegistrationType {
    Registered,
    Unregistered,
    CompositeDealer
}


