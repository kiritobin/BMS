﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Model
{
    /// <summary>
    /// 销售单头实体类
    /// </summary>
    public class SaleHead
    {
        /// <summary>
        /// 备注
        /// </summary>
        private string remarks;
        /// <summary>
        /// 销售单头Id
        /// </summary>
        private string saleHeadId;
        /// <summary>
        /// 销售任务Id
        /// </summary>
        private string saleTaskId;
        /// <summary>
        /// 品种数量
        /// </summary>
        private int kindsNum;
        /// <summary>
        /// 数量
        /// </summary>
        private int number;
        /// <summary>
        /// 总码洋
        /// </summary>
        private double allTotalPrice;
        /// <summary>
        /// 总实洋
        /// </summary>
        private double allRealPrice;
        /// <summary>
        /// 操作员Id
        /// </summary>
        private string userId;
        /// <summary>
        /// 操作员名称
        /// </summary>
        private string userName;
        /// <summary>
        /// 状态（0为未处理，1为已处理）
        /// </summary>
        private int state;
        /// <summary>
        /// 地区Id
        /// </summary>
        private int regionId;
        /// <summary>
        /// 地区名称
        /// </summary>
        private string regionName;
        /// <summary>
        /// 制单时间
        /// </summary>
        private DateTime dateTime;
        /// <summary>
        /// 零售支付状态
        /// </summary>
        private string payType;
        /// <summary>
        /// 零售用户唯一标示
        /// </summary>
        private string openid;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            get
            {
                return remarks;
            }

            set
            {
                remarks = value;
            }
        }
        /// <summary>
        /// 销售单头Id
        /// </summary>
        public string SaleHeadId
        {
            get
            {
                return saleHeadId;
            }

            set
            {
                saleHeadId = value;
            }
        }
        /// <summary>
        /// 销售任务ID
        /// </summary>
        public string SaleTaskId
        {
            get
            {
                return saleTaskId;
            }

            set
            {
                saleTaskId = value;
            }
        }
        /// <summary>
        /// 品种数
        /// </summary>
        public int KindsNum
        {
            get
            {
                return kindsNum;
            }

            set
            {
                kindsNum = value;
            }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number
        {
            get
            {
                return number;
            }

            set
            {
                number = value;
            }
        }
        /// <summary>
        /// 总码洋
        /// </summary>
        public double AllTotalPrice
        {
            get
            {
                return allTotalPrice;
            }

            set
            {
                allTotalPrice = value;
            }
        }
        /// <summary>
        /// 总实洋
        /// </summary>
        public double AllRealPrice
        {
            get
            {
                return allRealPrice;
            }

            set
            {
                allRealPrice = value;
            }
        }
        /// <summary>
        /// 操作员ID
        /// </summary>
        public string UserId
        {
            get
            {
                return userId;
            }

            set
            {
                userId = value;
            }
        }
        /// <summary>
        /// 操作员名称
        /// </summary>
        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
            }
        }
        /// <summary>
        /// 地区ID
        /// </summary>
        public int RegionId
        {
            get
            {
                return regionId;
            }

            set
            {
                regionId = value;
            }
        }
        /// <summary>
        /// 地区名称
        /// </summary>
        public string RegionName
        {
            get
            {
                return regionName;
            }

            set
            {
                regionName = value;
            }
        }
        /// <summary>
        /// 制单时间
        /// </summary>
        public DateTime DateTime
        {
            get
            {
                return dateTime;
            }

            set
            {
                dateTime = value;
            }
        }
        /// <summary>
        /// 零售支付状态
        /// </summary>
        public string PayType
        {
            get
            {
                return payType;
            }
            set
            {
                payType = value;
            }
        }
        /// <summary>
        /// 零售客户唯一标示
        /// </summary>
        public string OpenId
        {
            get
            {
                return openid;
            }
            set
            {
                openid = value;
            }
        }
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public SaleHead() { }

        /// <summary>
        /// 参数构造函数
        /// </summary>
        /// <param name="remarks">备注</param>
        /// <param name="saleHeadId">销售单头ID</param>
        /// <param name="saleTaskId">销售任务Id</param>
        /// <param name="kindsNum">品种数</param>
        /// <param name="number">数量</param>
        /// <param name="allTotalPrice">总码洋</param>
        /// <param name="allRealPrice">总实洋</param>
        /// <param name="userId">操作员ID</param>
        /// <param name="userName">操作员名称</param>
        /// <param name="state">状态（0为未操作，1为已操作）</param>
        /// <param name="regionId">地区Id</param>
        /// <param name="regionName">地区名称</param>
        /// <param name="dateTime">制单时间</param>
        /// <param name="payType">零售支付状态</param>
        /// <param name="openid">零售客户唯一标示</param>
        public SaleHead(string remarks,string saleHeadId, string saleTaskId, int kindsNum, int number, double allTotalPrice, double allRealPrice, string userId,string userName, int state, int regionId, string regionName, DateTime dateTime,string payType,string openid)
        {
            this.Remarks = remarks;
            SaleHeadId = saleHeadId;
            SaleTaskId = saleTaskId;
            KindsNum = kindsNum;
            Number = number;
            AllTotalPrice = allTotalPrice;
            AllRealPrice = allRealPrice;
            UserId = userId;
            UserName = userName;
            State = state;
            RegionId = regionId;
            RegionName = regionName;
            DateTime = dateTime;
            PayType = payType;
            OpenId = openid;
        }
    }
}
