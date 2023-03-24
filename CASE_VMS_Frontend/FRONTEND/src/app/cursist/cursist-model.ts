export interface CursistCourseDTO
{
    Cursist: Cursist;
    CourseId: number;
}

export interface Cursist{
    Id: number;
    FirstName: string;
    SurName: string;
    PaymentInfo: Partial<CursistPayInfo>;
}

export interface CursistPayInfo{
    Corporation: Partial<CorporateInfo>
    Private: Partial<PrivateInfo>
}

export interface CorporateInfo{

    CompanyName: string;
    Department: string;
    ContractNumber: string;
}

export interface PrivateInfo{
    StreetName: string;
    StreetNumber: string;
    PostalCode: string;
    City: string;
    BankAccount: string;
}