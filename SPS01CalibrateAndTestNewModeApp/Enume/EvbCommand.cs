using System;
using System.Collections.Generic;

namespace SPS01CalibrateAndTestNewModeApp.Enume
{
    public class EvbCommandOfEngine
    {
        /**
         *
         * STRT_CM     013C5B
                STRT_NM     020000
                STRT_MEAS   030000
                STRT_SENT   040000
                STOP_MEAS   050000
                STOP_SENT   060000
                SOFTRESET   070000命令字
         */
        // 静态
        public Dictionary<string, string> RunMode = new Dictionary<string, string>
        {
            { "STRT_CM", "013C5B" }, { "STRT_NM", "020000" }, { "STRT_MEAS", "030000" }, { "STRT_SENT", "040000" },
            { "STOP_MEAS", "050000" }, { "STOP_SENT", "060000" }, { "SOFTRESET", "070000" }
        };
    }

    public class EvbCommandOfWork
    {
        /*
             * RD_NVMREG_BYTE   10
             RD_NVMREG_BURST    11
             WR_NVMREG_BYTE     13
             WR_NVMREG_BURST    14
             RD_OUTMEM_BYTE     20
             RD_OUTMEM_BURST    21
             WR_OUTMEM_BYTE     22
             WR_OUTMEM_BURST    23
             OPEN_NVM           81
             CLOSE_NVM          82
             RD_NVM_BYTE        83
             RD_NVM_BURST       84
             ERS_NVM_BYTE       85
             ERS_NVM_BULK       86
             PROG_NVM_BYTE      87
             PROG_NVM_BULK      88
             CP_NVMTOREG        A0
             CP_REGTONVM        A1
             WR_NVMWR_AUTH      B0
             RD_NWMWR_VLD       B1
             GEN_NVMCRC         C3
             CHK_NVMCRC         E3
             */
        // 将以上对应关系写下来
        public Dictionary<string, string> WorkMode = new Dictionary<string, string>
        {
            { "RD_NVMREG_BYTE", "10" }, { "RD_NVMREG_BURST", "11" }, { "WR_NVMREG_BYTE", "13" },
            { "WR_NVMREG_BURST", "14" }, { "RD_OUTMEM_BYTE", "20" }, { "RD_OUTMEM_BURST", "21" },
            { "WR_OUTMEM_BYTE", "22" }, { "WR_OUTMEM_BURST", "23" }, { "OPEN_NVM", "81" }, { "CLOSE_NVM", "82" },
            { "RD_NVM_BYTE", "83" }, { "RD_NVM_BURST", "84" }, { "ERS_NVM_BYTE", "85" }, { "ERS_NVM_BULK", "86" },
            { "PROG_NVM_BYTE", "87" }, { "PROG_NVM_BULK", "88" }, { "CP_NVMTOREG", "A0" }, { "CP_REGTONVM", "A1" },
            { "WR_NVMWR_AUTH", "B0" }, { "RD_NWMWR_VLD", "B1" }, { "GEN_NVMCRC", "C3" }, { "CHK_NVMCRC", "E3" }
        };
    }

    public class EvbCommandOfRaw
    {
        public Dictionary<string, string> RawAddr = new Dictionary<string, string>
        {
            { "P1", "00" }, { "P2", "02" }, { "P3", "04" }, { "TSI", "06" }, { "TSE", "08" }, { "VDDA", "0A" },
            { "P1O", "0E" }, { "P2O", "10" }, { "TSIO", "12" }, { "TSEO", "14" }, { "P1DAC", "1E" }, { "P1SENT", "16" },
            { "P2SENT", "18" }, { "P1VOFF", "20" }, { "P1FG", "23" }, { "P2VOFF", "26" }, { "P2FG", "29" }
        };
    }

    public class EvbCommandOfConn
    {
        
        public Dictionary<string, string> ConnMode = new Dictionary<string, string> { { "OWI", "O" }, { "IIC", "I" } };
    }
}