﻿namespace COMP1640.ViewModels.Shared.Requests
{
    public class PagingRequest
    {
        private int _pageNo = 1;
        private int _pageSize = 5;
        public int PageNo
        {
            get { return _pageNo; }
            set { _pageNo = value; }
        }
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

    }
}
