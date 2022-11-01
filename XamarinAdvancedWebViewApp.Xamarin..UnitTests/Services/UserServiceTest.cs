using System.Diagnostics;
using System.Threading.Tasks;
using XamarinAdvancedWebViewApp.Xamarin.Core;
using XamarinAdvancedWebViewApp.Xamarin.Core.Constants;
using XamarinAdvancedWebViewApp.Xamarin.Core.Ioc;
using XamarinAdvancedWebViewApp.Xamarin.Core.Services.Mocks;
using Xamarin.Forms.Mocks;
using Xunit;

namespace XamarinAdvancedWebViewApp.Xamarin.UnitTests.Services
{
    public class UserServiceTest
    {
        public UserServiceTest()
        {
            GlobalSetting.Instance.BaseGatewayEndpoint = ApiConstants.BaseDevApiUrl;
            DependencyResolver.RegisterComponents(true);
            MockForms.Init();
            SettingsMockService.SetMockValues();
        }

        [Fact]
        public async void AuthenticateTest()
        {
            var testHelper = new TestHelper();

            var response = await testHelper.Login();

            Assert.True(response != null &&
                        response.IsSuccessful);

            Debug.WriteLine("AuthenticateTest called.  message is " + response.Message);
        }

        [Fact]
        public async Task VerifySmsCodeTest()
        {
            var testHelper = new TestHelper();

            var response = await testHelper.VerifySmsCode();

            Assert.True(response.IsSuccessful);

            Debug.WriteLine("VerifySmsCodeTest called");
        }
    }
}
