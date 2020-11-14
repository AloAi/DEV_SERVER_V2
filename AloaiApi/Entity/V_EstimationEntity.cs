//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aloai.Entity
{
    public class V_EstimationEntity
    {
        /// <summary>
        /// History Id.
        /// </summary>
        public decimal? historyId { get; set; }

        /// <summary>
        /// Exchange Id.
        /// </summary>
        public decimal exchangeId { get; set; }

        /// <summary>
        /// Complete date.
        /// </summary>
        public DateTime? completeDate { get; set; }

        /// <summary>
        /// History status
        /// </summary>
        public decimal? historyStatus { get; set; }

        /// <summary>
        /// Register user Id.
        /// </summary>
        public decimal? regUserId { get; set; }

        /// <summary>
        /// Register mode user.
        /// </summary>
        public decimal? regModeUser { get; set; }

        /// <summary>
        /// Job title
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Hirer's content request.
        /// </summary>
        public string requestContent { get; set; }

        /// <summary>
        /// Worker's introduction.
        /// </summary>
        public string introduction { get; set; }

        // Exchange
        /// <summary>
        /// Job Id
        /// </summary>
        public decimal jobId { get; set; }

        /// <summary>
        /// Worker Id
        /// </summary>
        public decimal workerId { get; set; }

        /// <summary>
        /// Worker name
        /// </summary>
        public string workerName { get; set; }

        /// <summary>
        /// Worker avatar
        /// </summary>
        public string workerAvatar { get; set; }

        /// <summary>
        /// Worker member type color.
        /// </summary>
        public string workerMemberTypeColor { get; set; }

        /// <summary>
        /// Worker score
        /// </summary>
        public decimal workerScore { get; set; }

        /// <summary>
        /// Worker Receive count.
        /// </summary>
        public decimal workerReceiveCnt { get; set; }

        /// <summary>
        /// Worker Cancel count.
        /// </summary>
        public decimal workerCancelCnt { get; set; }

        /// <summary>
        /// Worker Complete count.
        /// </summary>
        public decimal workerCompleteCnt { get; set; }

        /// <summary>
        /// Exchange Date
        /// </summary>
        public DateTime exchangeDate { get; set; }

        /// <summary>
        /// ExchangeStatus
        /// </summary>
        public decimal exchangeStatus { get; set; }

        /// <summary>
        /// Hirer Id
        /// </summary>
        public decimal hirerId { get; set; }

        /// <summary>
        /// Hirer name
        /// </summary>
        public string hirerName { get; set; }

        /// <summary>
        /// Hirer avatar
        /// </summary>
        public string hirerAvatar { get; set; }

        /// <summary>
        /// Hirer member type color.
        /// </summary>
        public string hirerMemberTypeColor { get; set; }

        /// <summary>
        /// Hirer score
        /// </summary>
        public decimal hirerScore { get; set; }

        /// <summary>
        /// Hirer Post count.
        /// </summary>
        public decimal hirerPostCnt { get; set; }

        /// <summary>
        /// Hirer Cancel count.
        /// </summary>
        public decimal hirerCancelCnt { get; set; }

        /// <summary>
        /// Hirer Complete count.
        /// </summary>
        public decimal hirerCompleteCnt { get; set; }

        // Estinmation
        /// <summary>
        /// Estimation Id.
        /// </summary>
        public decimal estimationId { get; set; }

        /// <summary>
        /// Estimator user Id.
        /// </summary>
        public decimal estimatorUserId { get; set; }

        /// <summary>
        /// Estimator user name.
        /// </summary>
        public string estimatorUserName { get; set; }

        /// <summary>
        /// Estimator's mode user.
        /// </summary>
        public decimal estModeUser { get; set; }

        public bool isSended { get; set; }

        /// <summary>
        /// Estimation date.
        /// </summary>
        public DateTime? estimationDate { get; set; }

        /// <summary>
        /// Score.
        /// </summary>
        public decimal? score { get; set; }

        /// <summary>
        /// Comment.
        /// </summary>
        public string comment { get; set; }

        public bool isReceived { get; set; }

        /// <summary>
        /// Estimation date received.
        /// </summary>
        public DateTime? estimationDateReceive { get; set; }

        /// <summary>
        /// Score received.
        /// </summary>
        public decimal? scoreReceive { get; set; }

        /// <summary>
        /// Comment received.
        /// </summary>
        public string commentReceive { get; set; }

        /// <summary>
        /// Worker's member type.
        /// </summary>
        public decimal? workerMemberType { get; set; }

        /// <summary>
        /// Worker's Company Id.
        /// </summary>
        public decimal? workerCompanyId { get; set; }

        /// <summary>
        /// Company Id.
        /// </summary>
        public decimal? hirerCompanyId { get; set; }
    }
}