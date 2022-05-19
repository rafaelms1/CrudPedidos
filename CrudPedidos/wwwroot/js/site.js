// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var data = document.querySelector("data");



    function Desconto() {
        var Desconto = document.getElementById('idDesconto');
        var Valor = document.getElementById('idValor');
        if (Desconto >= Valor) {
            alert('O Desconto não pode ser maior que o Valor!!');
            Desconto.focus();
            return false;
        }