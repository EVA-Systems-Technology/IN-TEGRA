$(document).ready(function () {
    $("#cep").mask("00.000-000");
});

$(document).ready(function () {

    function limpa_formulario_cep() {
        $("#estado").val("");
        $("#cidade").val("");
        $("#logradouro").val("");
        $("#bairro").val("");
        $("#complemento").val("");
    }

    $("#cep").blur(function () {

        var cep = $(this).val().replace(/\D/g, '');

        if (cep != "") {

            var validacep = /^[0-9]{8}$/;

            //Valida o formato do CEP.
            if (validacep.test(cep)) {

                $("#estado").val("...");
                $("#cidade").val("...");
                $("#logradouro").val("...");
                $("#bairro").val("...");
                $("#complemento").val("...");

                $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                    if (!("erro" in dados)) {
                        $("#estado").val(dados.uf);
                        $("#cidade").val(dados.localidade);
                        $("#logradouro").val(dados.logradouro);
                        $("#bairro").val(dados.bairro);
                        $("#complemento").val(dados.complemento);
                    }
                    else {
                        limpa_formulario_cep();
                        alert("CEP não encontrado.");
                    }
                });
            }
            else {
                limpa_formulario_cep();
                alert("Formato de CEP inválido.");
            }
        }
        else {
            limpa_formulario_cep();
        }
    });
});