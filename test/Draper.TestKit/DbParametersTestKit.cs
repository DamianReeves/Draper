using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using Xunit;

namespace Draper.TestKit
{
    public abstract class DbParametersTestKit<TDbParameter> where TDbParameter:DbParameter
    {
        private static readonly ConditionalWeakTable<Type,TestSettings> TestSettingsMap = new ConditionalWeakTable<Type, TestSettings>();
        
        protected DbParametersTestKit(IEnumerable<string> supportedProviderSpecificDbTypes, IEnumerable<string> unsupportedDbTypes = null)
        {
            if (supportedProviderSpecificDbTypes == null)
                throw new ArgumentNullException(nameof(supportedProviderSpecificDbTypes));
            var settings = TestSettingsMap.GetValue(typeof (TDbParameter), _=> 
                new TestSettings(supportedProviderSpecificDbTypes, unsupportedDbTypes));
            settings.SupportedProviderSpecificDbTypes = supportedProviderSpecificDbTypes;
            settings.UnsupportedProviderSpecificDbTypes = unsupportedDbTypes;
        }

        public static IEnumerable<object[]> GetSupportedDbTypes()
        {
            var testSettings = TestSettingsMap.GetValue(typeof (TDbParameter),_=>new TestSettings());
            foreach (var dbTypeName in testSettings.SupportedProviderSpecificDbTypes)
            {
                yield return new object[] {dbTypeName};
            }
        }

        [Theory]
        [MemberData("GetSupportedDbTypes")]
        public void SetProviderSpecificDbType()
        {
            
        }

        private class TestSettings
        {
            public TestSettings()
            {
                
            }

            public TestSettings(IEnumerable<string> supportedProviderSpecificDbTypes, IEnumerable<string> unsupportedProviderSpecificDbTypes=null)
            {
                if (supportedProviderSpecificDbTypes == null)
                    throw new ArgumentNullException(nameof(supportedProviderSpecificDbTypes));
                SupportedProviderSpecificDbTypes = supportedProviderSpecificDbTypes;
                UnsupportedProviderSpecificDbTypes = unsupportedProviderSpecificDbTypes ?? Enumerable.Empty<string>();
            }

            public IEnumerable<string> SupportedProviderSpecificDbTypes { get; set; }
            public IEnumerable<string> UnsupportedProviderSpecificDbTypes { get;  set; }
        }
    }
}
