//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2017 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

namespace Aloai.Enum
{
    /// <summary>
    ///  Mode user.
    /// </summary>
    public enum Mode
    {
        /// <summary>
        /// ワーカーモード
        /// </summary>
        Partner = 1,
        /// <summary>
        /// 求人の人モード
        /// </summary>
        Hirer = 2,
    }

    /// <summary>
    /// Exchange status.
    /// </summary>
    public enum ExchangeStatus
    {
        /// <summary>
        /// New
        /// </summary>
        New = 1,
        /// <summary>
        /// Finished
        /// </summary>
        Finished = 2,
    }

    /// <summary>
    /// History status.
    /// </summary>
    public enum HistoryStatus
    {
        /// <summary>
        /// Complete
        /// </summary>
        Complete = 1,
        /// <summary>
        /// Cancel
        /// </summary>
        Cancel = 2,
    }

    /// <summary>
    /// Job status.
    /// </summary>
    public enum JobStatus
    {
        /// <summary>
        /// New
        /// </summary>
        New = 1,
        /// <summary>
        /// Received
        /// </summary>
        Cancel = 2,
    }

    /// <summary>
    /// Notify type.
    /// </summary>
    public enum NotifyType
    {
        /// <summary>
        /// System
        /// </summary>
        System = 1,
        /// <summary>
        /// Estimation
        /// </summary>
        Estimation = 2,
        /// <summary>
        /// Job
        /// </summary>
        Job = 3,
    }

    /// <summary>
    /// Status.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Offline
        /// </summary>
        Offline = 0,
        /// <summary>
        /// Online
        /// </summary>
        Online = 1,
    }

    /// <summary>
    /// Data type.
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// Number
        /// </summary>
        Number = 1,
        /// <summary>
        /// String
        /// </summary>
        String = 2,
        /// <summary>
        /// Date
        /// </summary>
        Date = 3,
    }

    /// <summary>
    /// Account type.
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// Personal
        /// </summary>
        Personal = 1,
        /// <summary>
        /// Company
        /// </summary>
        Company = 2,
    }

    /// <summary>
    /// Member type.
    /// </summary>
    public enum MemberType
    {
        /// <summary>
        /// Free
        /// </summary>
        Free = 1,
        /// <summary>
        /// Secured account
        /// </summary>
        SecuredAccount = 2,

        ///// <summary>
        ///// Newbie
        ///// </summary>
        //Newbie = 1,
        ///// <summary>
        ///// Copper
        ///// </summary>
        //Copper = 2,
        ///// <summary>
        ///// Silver
        ///// </summary>
        //Silver = 3,
        ///// <summary>
        ///// Gold
        ///// </summary>
        //Gold = 4,
        ///// <summary>
        ///// Diamond
        ///// </summary>
        //Diamond = 5,
    }

    /// <summary>
    /// Score.
    /// </summary>
    public enum Score
    {
        /// <summary>
        /// Bad
        /// </summary>
        Bad = 1,
        /// <summary>
        /// Medium
        /// </summary>
        Medium = 2,
        /// <summary>
        /// Rather
        /// </summary>
        Rather = 3,
        /// <summary>
        /// Good
        /// </summary>
        Good = 4,
        /// <summary>
        /// Super
        /// </summary>
        Super = 5,
    }

    /// <summary>
    /// Readed flag.
    /// </summary>
    public enum ReadedFlg
    {
        /// <summary>
        /// New
        /// </summary>
        New = 0,
        /// <summary>
        /// Readed
        /// </summary>
        Readed = 1,
    }

    /// <summary>
    /// Show flag.
    /// </summary>
    public enum ShowFlg
    {
        /// <summary>
        /// Hidden
        /// </summary>
        Hidden = 0,
        /// <summary>
        /// Show
        /// </summary>
        Show = 1,
    }

    /// <summary>
    /// Favourite flag.
    /// </summary>
    public enum FavouriteFlag
    {
        /// <summary>
        /// Not like.
        /// </summary>
        NotLike = 0,
        /// <summary>
        /// Like
        /// </summary>
        Like = 1,
    }

    /// <summary>
    /// Block flag.
    /// </summary>
    public enum BlockFlag
    {
        /// <summary>
        /// Not block.
        /// </summary>
        NotBlock = 0,
        /// <summary>
        /// Blocked
        /// </summary>
        Blocked = 1,
    }

    /// <summary>
    /// Delete flag.
    /// </summary>
    public enum DeleteFlag
    {
        /// <summary>
        /// Using.
        /// </summary>
        Using = 0,
        /// <summary>
        /// Deleted
        /// </summary>
        Deleted = 1,
    }

    /// <summary>
    /// Allow update info.
    /// </summary>
    public enum AllowUpdateInfo
    {
        /// <summary>
        /// Not allow.
        /// </summary>
        NotAllow = 0,
        /// <summary>
        /// Allow
        /// </summary>
        Allow = 1,
    }

    /// <summary>
    /// Image type.
    /// </summary>
    public enum ImageType
    {
        /// <summary>
        /// Profile.
        /// </summary>
        Profile = 1,
        /// <summary>
        /// Job
        /// </summary>
        Job = 2,
        /// <summary>
        /// Avatar
        /// </summary>
        Avatar = 3,
    }

    /// <summary>
    /// Call type.
    /// </summary>
    public enum CallType
    {
        /// <summary>
        /// Call.
        /// </summary>
        Call = 1,
        /// <summary>
        /// Receive
        /// </summary>
        Receive = 2,
    }
}