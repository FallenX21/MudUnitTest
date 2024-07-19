using Bunit;
using FluentAssertions;
using MudBlazor;
using MudBlazor.Services;

namespace UnitTest
{
    public class ComponentTest:TestContext
    {
        [Fact]
        public void Test1()
        {
            JSInterop.SetupVoid("mudKeyInterceptor.updatekey", _ => true);
            JSInterop.Setup<int>("mudpopoverHelper.countProviders");
            JSInterop.SetupVoid("modPopover.initialize", _ => true);
            JSInterop.SetupVoid("mudKeyInterceptor.connect", _ => true);
            JSInterop.SetupVoid("mudPopover.initialize", "mudblazor-main-content", 0);
            
            Services.AddMudServices();


            var component =RenderComponent<MudUnitTest.Client.Pages.Component>();
            var popoverProvier = RenderComponent<MudPopoverProvider>();

            component.RenderCount.Should().Be(1);

            component.Find("div.mud-input-control").Click();

            component.RenderCount.Should().NotBe(1);

            component.WaitForAssertion(() =>
            {
                var items = popoverProvier.FindAll("div.mud-list-item").ToList();
                items.Count().Should().Be(4);
            });
        }
    }
}