using LibraryManagement.DTOs;
using LibraryManagement.Models;
using LibraryManagement.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;


namespace LibraryManagement.Services
{
    public class ParameterService
    {

        private ParameterService() { }
        private static ParameterService _ins;
        public static ParameterService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new ParameterService();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public int GetRuleValue(Rules RULES)
        {
            try
            {
                var param = DataProvider.Ins.DB.Parameters.Find(RULES.Value);
                return param.value;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool SetRuleValue(Rules RULES, int value)
        {
            try
            {
                var param = DataProvider.Ins.DB.Parameters.Find(RULES.Value);
                param.value = (short)value;

                DataProvider.Ins.DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
