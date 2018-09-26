export class UserSetting { //utility class for setting and reading user defined values in local storage


    public static getInventorySettings(): boolean {
        let inventory = JSON.parse(localStorage.getItem('inventory'));
        return inventory ? Boolean(inventory) : false;
    }

    public static setInventorySettings(inventorySelection: boolean) {

        localStorage.setItem('inventory', String(inventorySelection));
    }


    public static getYearSettings(): number {

        let year = JSON.parse(localStorage.getItem('year'));

        return year ? +year : new Date().getFullYear();
    }
    public static setYearSettings(year: number) {

        localStorage.setItem('year', String(year));
    }



   


    public static getYearDisplaySettings(): string {

        let year = JSON.parse(localStorage.getItem('year')); //eg.2017

        let nextYearLastTwodigit = (year % 100) + 1; // if 2017/100,rem=17+1=18

        let financialYear = year + '-' + nextYearLastTwodigit; // '2017' + '-' + '18' (by concanating) = 2017-18(Display)

        return financialYear;

    }


    public static getSalesMonth(): number {

        let salesMonthId = JSON.parse(localStorage.getItem('salesMonthId'));

        return salesMonthId ? +salesMonthId : new Date().getMonth();
    }
    public static setSalesMonth(salesMonthId: number) {

        localStorage.setItem('salesMonthId', String(salesMonthId));
    }

    public static getSalesNoteMonth(): number {

        let salesMonthId = JSON.parse(localStorage.getItem('salesNoteMonth'));

        return salesMonthId ? +salesMonthId : new Date().getMonth();
    }
    public static setSalesNoteMonth(salesMonthId: number) {

        localStorage.setItem('salesNoteMonth', String(salesMonthId));
    }

    public static getAccountingUnit(): number {

        let accountingUnitId = JSON.parse(localStorage.getItem('accountingUnitId'));

        return accountingUnitId;
    }
    public static setAccountingUnit(accountingUnitId: number) {

        localStorage.setItem('accountingUnitId', String(accountingUnitId));
    }

    public static getSalesAdvanceMonth(): number {

        let salesMonthId = JSON.parse(localStorage.getItem('salesAdvanceMonth'));

        return salesMonthId ? +salesMonthId : new Date().getMonth();
    }
    public static setSalesAdvanceMonth(salesMonthId: number) {

        localStorage.setItem('salesAdvanceMonth', String(salesMonthId));
    }

    public static getReportingMonth(): number {

        let reportingMonthId = JSON.parse(localStorage.getItem('reportingMonthId'));

        return reportingMonthId ? +reportingMonthId : new Date().getMonth();
    }
    
    public static setReportingMonth(reportingMonthId: number) {

        localStorage.setItem('reportingMonthId', String(reportingMonthId));
    }



    public static getPurchaseMonth(): number {

        let salesMonthId = JSON.parse(localStorage.getItem('purchaseMonthId'));

        return salesMonthId ? +salesMonthId : new Date().getMonth();
    }
    public static setPurchaseMonth(salesMonthId: number) {

        localStorage.setItem('purchaseMonthId', String(salesMonthId));
    }

    public static getPurchaseNoteMonth(): number {

        let salesMonthId = JSON.parse(localStorage.getItem('purchaseNoteMonth'));

        return salesMonthId ? +salesMonthId : new Date().getMonth();
    }
    public static setPurchaseNoteMonth(salesMonthId: number) {

        localStorage.setItem('purchaseNoteMonth', String(salesMonthId));
    }
    public static getPurchaseAdvanceMonth(): number {

        let salesMonthId = JSON.parse(localStorage.getItem('purchaseAdvanceMonth'));

        return salesMonthId ? +salesMonthId : new Date().getMonth();
    }
    public static setPurchaseAdvanceMonth(salesMonthId: number) {

        localStorage.setItem('purchaseAdvanceMonth', String(salesMonthId));
    }


   

    



}