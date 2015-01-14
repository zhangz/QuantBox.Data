﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuantBox.Data.Serializer
{
    [ProtoContract]
    public class DepthTick
    {
        [ProtoMember(1, DataFormat = DataFormat.ZigZag)]
        public int BidPrice1;
        [ProtoMember(2, DataFormat = DataFormat.ZigZag)]
        public int BidSize1;
        [ProtoMember(3, DataFormat = DataFormat.ZigZag)]
        public int AskPrice1;
        [ProtoMember(4, DataFormat = DataFormat.ZigZag)]
        public int AskSize1;
        [ProtoMember(5, DataFormat = DataFormat.ZigZag)]
        public int BidPrice2;
        [ProtoMember(6, DataFormat = DataFormat.ZigZag)]
        public int BidSize2;
        [ProtoMember(7, DataFormat = DataFormat.ZigZag)]
        public int AskPrice2;
        [ProtoMember(8, DataFormat = DataFormat.ZigZag)]
        public int AskSize2;
        [ProtoMember(9, DataFormat = DataFormat.ZigZag)]
        public int BidPrice3;
        [ProtoMember(10, DataFormat = DataFormat.ZigZag)]
        public int BidSize3;
        [ProtoMember(11, DataFormat = DataFormat.ZigZag)]
        public int AskPrice3;
        [ProtoMember(12, DataFormat = DataFormat.ZigZag)]
        public int AskSize3;

        /// <summary>
        /// 指向下块多档行情
        /// </summary>
        [ProtoMember(13)]
        public DepthTick Next;

        [ProtoMember(14, DataFormat = DataFormat.ZigZag)]
        public int BidCount1;
        [ProtoMember(15, DataFormat = DataFormat.ZigZag)]
        public int AskCount1;

        [ProtoMember(16, DataFormat = DataFormat.ZigZag)]
        public int BidCount2;
        [ProtoMember(17, DataFormat = DataFormat.ZigZag)]
        public int AskCount2;
        [ProtoMember(18, DataFormat = DataFormat.ZigZag)]
        public int BidCount3;
        [ProtoMember(19, DataFormat = DataFormat.ZigZag)]
        public int AskCount3;
    }

    [ProtoContract]
    public class StaticInfo
    {
        /// <summary>
        /// 与上一笔的LowerLimitPrice比较
        /// </summary>
        [ProtoMember(1, DataFormat = DataFormat.ZigZag)]
        public int LowerLimitPrice;
        /// <summary>
        /// 与上一笔的UpperLimitPrice比较
        /// </summary>
        [ProtoMember(2, DataFormat = DataFormat.ZigZag)]
        public int UpperLimitPrice;
        /// <summary>
        /// 实际数*100，因为IF交割日结算价两位小数
        /// </summary>
        [ProtoMember(3, DataFormat = DataFormat.ZigZag)]
        public int SettlementPrice;
        /// <summary>
        /// 合约乘数
        /// </summary>
        [ProtoMember(4)]
        public int Multiplier;
        /// <summary>
        /// 合约名称
        /// </summary>
        [ProtoMember(5)]
        public string Symbol;
        /// <summary>
        /// 交易所
        /// </summary>
        [ProtoMember(6)]
        public string Exchange;
    }

    [ProtoContract]
    public class BarInfo
    {
        /// <summary>
        /// 与上一笔的Open比较
        /// </summary>
        [ProtoMember(1, DataFormat = DataFormat.ZigZag)]
        public int Open;
        /// <summary>
        /// 与上一笔的High比较
        /// </summary>
        [ProtoMember(2, DataFormat = DataFormat.ZigZag)]
        public int High;
        /// <summary>
        /// 与上一笔的Low比较
        /// </summary>
        [ProtoMember(3, DataFormat = DataFormat.ZigZag)]
        public int Low;
        /// <summary>
        /// 与上一笔的Close比较
        /// </summary>
        [ProtoMember(4, DataFormat = DataFormat.ZigZag)]
        public int Close;
        /// <summary>
        /// 与上一笔的BarSize比较
        /// </summary>
        [ProtoMember(5, DataFormat = DataFormat.ZigZag)]
        public int BarSize;
    }

    /// <summary>
    /// 配置信息，最关键的地方
    /// 如果为空表示差分数据，不为空表示快照数据
    /// </summary>
    [ProtoContract]
    public class ConfigInfo
    {
        /// <summary>
        /// 格式版本
        /// </summary>
        [ProtoMember(1)]
        public int Version;
        /// <summary>
        /// 实际值要先进行处理
        /// </summary>
        [ProtoMember(2)]
        public int TickSize;
        /// <summary>
        /// TickSize乘数，实际值*TickSizeMultiplier
        /// </summary>
        [ProtoMember(3)]
        public double TickSizeMultiplier;
        /// <summary>
        /// 结算价乘数，实际值*SettlementPriceMultiplier/TickSize
        /// </summary>
        [ProtoMember(4)]
        public int SettlementPriceMultiplier;
        /// <summary>
        /// 均价乘数，实际值*AveragePriceMultiplier/TickSize
        /// </summary>
        [ProtoMember(5)]
        public int AveragePriceMultiplier;
        /// <summary>
        /// 成交额乘数，实际值/TurnoverMultiplier,只保存了万元，在深虚值期权中会丢失信息
        /// </summary>
        [ProtoMember(6)]
        public double TurnoverMultiplier;
        /// <summary>
        /// ssf默认时间差，股指是500ms，Time_____ssf__会算出大量的5，默认减去此数还原成0
        /// </summary>
        [ProtoMember(7)]
        public int Time_ssf_Diff;

        public ConfigInfo Default()
        {
            Version = 1;
            TickSize = 1;
            TickSizeMultiplier = 10000.0;
            SettlementPriceMultiplier = 100;
            AveragePriceMultiplier = 100;
            TurnoverMultiplier = 10000;
            Time_ssf_Diff = 0;

            return this;
        }

        public ConfigInfo Flat()
        {
            Version = 0;
            TickSize = 1;
            TickSizeMultiplier = 1;
            SettlementPriceMultiplier = 1;
            AveragePriceMultiplier = 1;
            TurnoverMultiplier = 1;
            Time_ssf_Diff = 0;

            return this;
        }
    }

    [ProtoContract]
    public class PbTick
    {
        /// <summary>
        /// 与上一笔的比
        /// </summary>
        [ProtoMember(1, DataFormat = DataFormat.ZigZag)]
        public int LastPrice;
        /// <summary>
        /// 成交量
        /// </summary>
        [ProtoMember(2, DataFormat = DataFormat.ZigZag)]
        public long Volume;
        /// <summary>
        /// 持仓量
        /// </summary>
        [ProtoMember(3, DataFormat = DataFormat.ZigZag)]
        public long OpenInterest;
        /// <summary>
        /// 成交额,实际值*1000
        /// </summary>
        [ProtoMember(4, DataFormat = DataFormat.ZigZag)]
        public long Turnover;
        /// <summary>
        /// 均价,实际值/10000
        /// </summary>
        [ProtoMember(5, DataFormat = DataFormat.ZigZag)]
        public int AveragePrice;

        /// <summary>
        /// 交易日
        /// </summary>
        [ProtoMember(6, DataFormat = DataFormat.ZigZag)]
        public int TradingDay;      
        /// <summary>
        /// 实际日期
        /// </summary>
        [ProtoMember(7, DataFormat = DataFormat.ZigZag)]
        public int ActionDay;
        [ProtoMember(8, DataFormat = DataFormat.ZigZag)]
        public int Time_HHmm;
        [ProtoMember(9, DataFormat = DataFormat.ZigZag)]
        public int Time_____ssf__;
        [ProtoMember(10, DataFormat = DataFormat.ZigZag)]
        public int Time________ff;

        /// <summary>
        /// N档数据
        /// </summary>
        [ProtoMember(11)]
        public DepthTick Depth1_3;
        /// <summary>
        /// Bar数据或高开低收
        /// </summary>
        [ProtoMember(12)]
        public BarInfo Bar;
        /// <summary>
        /// 涨跌停价格及结算价
        /// </summary>
        [ProtoMember(13)]
        public StaticInfo Static;
        /// <summary>
        /// 配置信息，有就代表是快照，而不是差分
        /// </summary>
        [ProtoMember(14)]
        public ConfigInfo Config;
    }
}
