export interface IGstr1 {

    gstin: string,
    fp: number,
    gt: number,
    curGt: number,
    b2b: IB2b[],
    b2cl: IB2cl[],
    b2cs: IB2c[],
    exp:IExport[],
    at: IAdvanceReceive[],
    cdnr: ICreditDebitNoteRegister[],
    cdnur: ICreditDebitNoteUnregister[],
    hsn: IHsn[],
    
}

export interface IB2b {

    ctin: string,
    inv: IInv[],
}
export interface IB2cl {
    pos: number,
    inv: IInv[]
}
export interface IB2c {


    splyTy: string,
    pos: number,
    txval: number,
    rt: number,
    iamt: number,
    camt: number,
    samt: number,
    csamt: number,
}
export interface IAdvanceReceive {
    splyTy: string,
    pos: number,
    itms: IItems[]
}

export interface ICreditDebitNoteRegister {
    ctin: string,
    nt: INt[],
}
export interface ICreditDebitNoteUnregister {
    ntNum: number,
    ntDt: Date,
    ntty: string,
    rsn: string,
    pGst: string,
    typ: string,
    inum: number,
    idt: Date,
    val: number,
    itms: IItems[]
}

export interface IHsn {
    data: IData,
   

}

export interface IItems {
    itmDet: IITemDetail
}
export interface IITemDetail {
    txval: number,
    rt: number,
    iamt: number,
    camt: number,
    samt: number,
    csamt: number,
    adAmt: number,
}

export interface IInv {
    inum: number,
    
    idt: Date,
    val: number,
    pos: number,

    itms: IItems[],
}

export interface INt {
    ntNum: number,
    ntDt: Date,
    ntty: string,
    rsn: string,
    pGst: string,
    inum: number,
    idt: Date,
    val: number,
    itms: IItems[],
}

export interface IData {
    num: number,
    hsnSc: string,
    uqc: string,
    qty: number,
    txval: number,
    val: number,
    iamt: number,
    csamt: number
    
}
export interface IExpInv {
    inum: number,
    idt: Date,
    val: number,
    sbpcode: string,
    sbnum: string,
    sbdt: string,

    itms: IItems[],
}
export interface IExport {
    expTyp: string,
    inv: IExpInv,

}


/*{
    "gstin": "10ABFPG864M1ZN",
        "fp": "92017",
            "gt": 0.0,
                "cur_gt": 0.0,
                    "b2b": [
                        {
                            "ctin": "10AAWPA9679D1ZJ",
                            "inv": [
                                {
                                    "inum": "9",
                                    "idt": "23-09-2017",
                                    "val": 3008.25,
                                    "pos": "10",
                                    "itms": [
                                        {
                                            "num": 1,
                                            "itm_det": {
                                                "txval": 2865.0,
                                                "rt": 5.0,
                                                "camt": 71.62,
                                                "samt": 71.62
                                            }
                                        }
                                    ]
                                }
                            ]
                        },
                        {
                            "ctin": "10ABQPG2968Q1Z4",
                            "inv": [
                                {
                                    "inum": "10",
                                    "idt": "23-09-2017",
                                    "val": 3213.0,
                                    "pos": "10",
                                    "itms": [
                                        {
                                            "num": 1,
                                            "itm_det": {
                                                "txval": 3060.0,
                                                "rt": 5.0,
                                                "camt": 76.5,
                                                "samt": 76.5
                                            }
                                        }
                                    ]
                                }
                            ]
                        },
                        {
                            "ctin": "10AAQPA8105J1Z1",
                            "inv": [
                                {
                                    "inum": "11",
                                    "idt": "19-09-2017",
                                    "val": 1758.75,
                                    "pos": "10",
                                    "itms": [
                                        {
                                            "num": 1,
                                            "itm_det": {
                                                "txval": 1675.0,
                                                "rt": 5.0,
                                                "camt": 41.88,
                                                "samt": 41.88
                                            }
                                        }
                                    ]
                                },
                                {
                                    "inum": "12",
                                    "idt": "23-09-2017",
                                    "val": 4746.0,
                                    "pos": "10",
                                    "itms": [
                                        {
                                            "num": 1,
                                            "itm_det": {
                                                "txval": 4520.0,
                                                "rt": 5.0,
                                                "camt": 113.0,
                                                "samt": 113.0
                                            }
                                        }
                                    ]
                                },
                                {
                                    "inum": "14",
                                    "idt": "23-09-2017",
                                    "val": 703.5,
                                    "pos": "10",
                                    "itms": [
                                        {
                                            "num": 1,
                                            "itm_det": {
                                                "txval": 670.0,
                                                "rt": 5.0,
                                                "camt": 16.75,
                                                "samt": 16.75
                                            }
                                        }
                                    ]
                                }
                            ]
                        },
                        {
                            "ctin": "10AKVPG7843A1ZD",
                            "inv": [
                                {
                                    "inum": "13",
                                    "idt": "23-09-2017",
                                    "val": 5964.0,
                                    "pos": "10",
                                    "itms": [
                                        {
                                            "num": 1,
                                            "itm_det": {
                                                "txval": 5680.0,
                                                "rt": 5.0,
                                                "camt": 142.0,
                                                "samt": 142.0
                                            }
                                        }
                                    ]
                                }
                            ]
                        },
                        {
                            "ctin": "10ADHPG0151C1ZN",
                            "inv": [
                                {
                                    "inum": "15",
                                    "idt": "23-09-2017",
                                    "val": 4725.0,
                                    "pos": "10",
                                    "itms": [
                                        {
                                            "num": 1,
                                            "itm_det": {
                                                "txval": 4500.0,
                                                "rt": 5.0,
                                                "camt": 112.5,
                                                "samt": 112.5
                                            }
                                        }
                                    ]
                                }
                            ]
                        },
                        {
                            "ctin": "10ADLPS8979C1Z7",
                            "inv": [
                                {
                                    "inum": "16",
                                    "idt": "23-09-2017",
                                    "val": 2452.8,
                                    "pos": "10",
                                    "itms": [
                                        {
                                            "num": 1,
                                            "itm_det": {
                                                "txval": 2336.0,
                                                "rt": 5.0,
                                                "camt": 58.4,
                                                "samt": 58.4
                                            }
                                        }
                                    ]
                                }
                            ]
                        }
                    ],
                        "b2cl": [
                            {
                                "pos": "01",
                                "inv": [
                                    {
                                        "inum": 18,
                                        "idt": "25-09-2017",
                                        "val": "251315.00",
                                        "itms": [
                                            {
                                                "num": 1,
                                                "itm_det": {
                                                    "txval": 182500.0,
                                                    "rt": 5.0,
                                                    "iamt": 9125.0
                                                }
                                            },
                                            {
                                                "num": 2,
                                                "itm_det": {
                                                    "txval": 47500.0,
                                                    "rt": 18.0,
                                                    "iamt": 8550.0
                                                }
                                            },
                                            {
                                                "num": 3,
                                                "itm_det": {
                                                    "txval": 3250.0,
                                                    "rt": 12.0,
                                                    "iamt": 390.0
                                                }
                                            }
                                        ]
                                    }
                                ]
                            },
                            {
                                "pos": "24",
                                "inv": [
                                    {
                                        "inum": 21,
                                        "idt": "25-09-2017",
                                        "val": "1365000.00",
                                        "itms": [
                                            {
                                                "num": 1,
                                                "itm_det": {
                                                    "txval": 1300000.0,
                                                    "rt": 5.0,
                                                    "iamt": 65000.0
                                                }
                                            }
                                        ]
                                    }
                                ]
                            },
                            {
                                "pos": "29",
                                "inv": [
                                    {
                                        "inum": 19,
                                        "idt": "25-09-2017",
                                        "val": "596400.00",
                                        "itms": [
                                            {
                                                "num": 1,
                                                "itm_det": {
                                                    "txval": 157500.0,
                                                    "rt": 12.0,
                                                    "iamt": 18900.0
                                                }
                                            },
                                            {
                                                "num": 2,
                                                "itm_det": {
                                                    "txval": 400000.0,
                                                    "rt": 5.0,
                                                    "iamt": 20000.0
                                                }
                                            }
                                        ]
                                    }
                                ]
                            },
                            {
                                "pos": "32",
                                "inv": [
                                    {
                                        "inum": 20,
                                        "idt": "25-09-2017",
                                        "val": "2593650.00",
                                        "itms": [
                                            {
                                                "num": 1,
                                                "itm_det": {
                                                    "txval": 282500.0,
                                                    "rt": 12.0,
                                                    "iamt": 33900.0
                                                }
                                            },
                                            {
                                                "num": 2,
                                                "itm_det": {
                                                    "txval": 1635000.0,
                                                    "rt": 5.0,
                                                    "iamt": 81750.0
                                                }
                                            },
                                            {
                                                "num": 3,
                                                "itm_det": {
                                                    "txval": 475000.0,
                                                    "rt": 18.0,
                                                    "iamt": 85500.0
                                                }
                                            }
                                        ]
                                    }
                                ]
                            }
                        ],
                            "b2cs": [
                                {
                                    "sply_ty": "IntraState",
                                    "pos": "10",
                                    "taxval": 14272.374999999996,
                                    "rt": 5.0,
                                    "camt": 356.809375,
                                    "samt": 356.809375
                                },
                                {
                                    "sply_ty": "IntraState",
                                    "pos": "10",
                                    "taxval": 720.40000000000009,
                                    "rt": 18.0,
                                    "camt": 64.836,
                                    "samt": 64.836
                                },
                                {
                                    "sply_ty": "IntraState",
                                    "pos": "10",
                                    "taxval": 2830.0,
                                    "rt": 0.0
                                },
                                {
                                    "sply_ty": "InterState",
                                    "pos": "37",
                                    "taxval": 32500.0,
                                    "rt": 5.0,
                                    "iamt": 1625.0
                                }
                            ],
                                "at": [
                                    {
                                        "pos": "02",
                                        "sply_ty": "InterState",
                                        "itms": [
                                            {
                                                "num": 1,
                                                "itm_det": {
                                                    "ad_amt": 52.5,
                                                    "txval": 0.0,
                                                    "rt": 5.0,
                                                    "iamt": 2.5
                                                }
                                            }
                                        ]
                                    },
                                    {
                                        "pos": "10",
                                        "sply_ty": "IntraState",
                                        "itms": [
                                            {
                                                "num": 1,
                                                "itm_det": {
                                                    "ad_amt": 75.6,
                                                    "txval": 0.0,
                                                    "rt": 5.0
                                                }
                                            }
                                        ]
                                    }
                                ],
                                    "cdnr": [
                                        {
                                            "ctin": "10AAQPA8105J1Z1",
                                            "nt": [
                                                {
                                                    "nt_num": "6",
                                                    "nt_dt": "12-09-2017",
                                                    "ntty": "Credit",
                                                    "rsn": "Others",
                                                    "p_gst": "No",
                                                    "inum": "11",
                                                    "idt": "23-09-2017",
                                                    "val": "1758.75",
                                                    "itms": [
                                                        {
                                                            "num": 1,
                                                            "itm_det": {
                                                                "txval": 66.0,
                                                                "rt": 5.0
                                                            }
                                                        }
                                                    ]
                                                }
                                            ]
                                        }
                                    ],
                                        "cdnur": [
                                            {
                                                "nt_num": "5",
                                                "nt_dt": "03-09-2017",
                                                "ntty": "Credit",
                                                "rsn": "Others",
                                                "p_gst": "No",
                                                "typ": "B2C",
                                                "inum": "6",
                                                "idt": "23-09-2017",
                                                "val": "2960.20",
                                                "itms": [
                                                    {
                                                        "num": 1,
                                                        "itm_det": {
                                                            "txval": 6666.0,
                                                            "rt": 5.0
                                                        }
                                                    }
                                                ]
                                            }
                                        ],
                                            "hsn": [
                                                {
                                                    "data": {
                                                        "num": 1,
                                                        "hsn_sc": "08013220",
                                                        "uqc": "KG",
                                                        "qty": 2.0,
                                                        "txval": 1638.12,
                                                        "val": 1638.12,
                                                        "iamt": "81.906",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 2,
                                                        "hsn_sc": "08062010",
                                                        "uqc": "KG",
                                                        "qty": 3.5,
                                                        "txval": 533.365,
                                                        "val": 533.365,
                                                        "iamt": "26.66825",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 3,
                                                        "hsn_sc": "09023020",
                                                        "uqc": "PCS",
                                                        "qty": 20.0,
                                                        "txval": 1200.0,
                                                        "val": 1200.0,
                                                        "iamt": "60",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 4,
                                                        "hsn_sc": "0904",
                                                        "uqc": "PCS",
                                                        "qty": 5.0,
                                                        "txval": 228.6,
                                                        "val": 228.6,
                                                        "iamt": "11.43",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 5,
                                                        "hsn_sc": "1507",
                                                        "uqc": "PCS",
                                                        "qty": 10.0,
                                                        "txval": 800.0,
                                                        "val": 800.0,
                                                        "iamt": "40",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 6,
                                                        "hsn_sc": "15079010",
                                                        "uqc": "PCS",
                                                        "qty": 44.0,
                                                        "txval": 3562.24,
                                                        "val": 3562.24,
                                                        "iamt": "178.112",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 7,
                                                        "hsn_sc": "1514",
                                                        "uqc": "PCS",
                                                        "qty": 27.0,
                                                        "txval": 2685.8900000000003,
                                                        "val": 2685.8900000000003,
                                                        "iamt": "134.2945",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 8,
                                                        "hsn_sc": "15149120",
                                                        "uqc": "PCS",
                                                        "qty": 17.0,
                                                        "txval": 1538.16,
                                                        "val": 1538.16,
                                                        "iamt": "76.908",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 9,
                                                        "hsn_sc": "1517",
                                                        "uqc": "PCS",
                                                        "qty": 10.0,
                                                        "txval": 809.59999999999991,
                                                        "val": 809.59999999999991,
                                                        "iamt": "40.48",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 10,
                                                        "hsn_sc": "1904",
                                                        "uqc": "PCS",
                                                        "qty": 10.0,
                                                        "txval": 720.40000000000009,
                                                        "val": 720.40000000000009,
                                                        "iamt": "129.672",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 11,
                                                        "hsn_sc": "3922",
                                                        "uqc": "PCS",
                                                        "qty": 500.0,
                                                        "txval": 400000.0,
                                                        "val": 400000.0,
                                                        "iamt": "20000",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 12,
                                                        "hsn_sc": "4202",
                                                        "uqc": "PCS",
                                                        "qty": 555.0,
                                                        "txval": 193250.0,
                                                        "val": 193250.0,
                                                        "iamt": "23190",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 13,
                                                        "hsn_sc": "4404",
                                                        "uqc": "PCS",
                                                        "qty": 50.0,
                                                        "txval": 60000.0,
                                                        "val": 60000.0,
                                                        "iamt": "3000",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 14,
                                                        "hsn_sc": "4820",
                                                        "uqc": "PCS",
                                                        "qty": 500.0,
                                                        "txval": 250000.0,
                                                        "val": 250000.0,
                                                        "iamt": "30000",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 15,
                                                        "hsn_sc": "5208",
                                                        "uqc": "MTR.",
                                                        "qty": 66.0,
                                                        "txval": 5396.0,
                                                        "val": 5396.0,
                                                        "iamt": "269.8",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 16,
                                                        "hsn_sc": "5208",
                                                        "uqc": "PCS",
                                                        "qty": 5118.0,
                                                        "txval": 1317532.5,
                                                        "val": 1317532.5,
                                                        "iamt": "65876.625",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 17,
                                                        "hsn_sc": "5303",
                                                        "uqc": "PCS",
                                                        "qty": 550.0,
                                                        "txval": 1635000.0,
                                                        "val": 1635000.0,
                                                        "iamt": "81750",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 18,
                                                        "hsn_sc": "5407",
                                                        "uqc": "PCS",
                                                        "qty": 5.0,
                                                        "txval": 1162.5,
                                                        "val": 1162.5,
                                                        "iamt": "58.125",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 19,
                                                        "hsn_sc": "5515",
                                                        "uqc": "PCS",
                                                        "qty": 515.0,
                                                        "txval": 33715.0,
                                                        "val": 33715.0,
                                                        "iamt": "1685.75",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 20,
                                                        "hsn_sc": "9701",
                                                        "uqc": "PCS",
                                                        "qty": 50.0,
                                                        "txval": 122500.0,
                                                        "val": 122500.0,
                                                        "iamt": "6125",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 21,
                                                        "hsn_sc": "N/A",
                                                        "uqc": "PCS",
                                                        "qty": 550.0,
                                                        "txval": 522500.0,
                                                        "val": 522500.0,
                                                        "iamt": "94050",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 22,
                                                        "hsn_sc": "OTHERS",
                                                        "uqc": "KG",
                                                        "qty": 45.0,
                                                        "txval": 2830.0,
                                                        "val": 2830.0,
                                                        "iamt": "0",
                                                        "csamt": "0"
                                                    }
                                                },
                                                {
                                                    "data": {
                                                        "num": 23,
                                                        "hsn_sc": "OTHERS",
                                                        "uqc": "PCS",
                                                        "qty": 40.0,
                                                        "txval": 1276.4,
                                                        "val": 1276.4,
                                                        "iamt": "63.82",
                                                        "csamt": "0"
                                                    }
                                                }
                                            ]
}


*/