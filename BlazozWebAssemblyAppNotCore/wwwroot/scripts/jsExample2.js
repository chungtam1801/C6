var jsFunctions = {};
jsFunctions.calculateSquareRoot = function () {
    const number = prompt("Enter your number:");
    DotNet.invokeMethodAsync("BlazozWebAssemblyAppNotCore","CalculateSquareRoot", parseInt(number)).then(result => {
        var el = document.getElementById("string-result");
        el.innerHTML = result;
    });
}