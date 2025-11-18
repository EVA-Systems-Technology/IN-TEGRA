const caixas = document.querySelectorAll('.categoria');
const cards = document.querySelectorAll('.card');

caixas.forEach(caixa => {
    caixa.addEventListener('change', () => {
        let selecionada = null;


        caixas.forEach(caixa => {

            if (caixa.checked) selecionada = caixa.value;
        });

        if (selecionada === null) {
            cards.forEach(card => card.parentElement.style.display = "block");
            return; 
        }

        cards.forEach(card => {
            if (card.getAttribute("data-categoria") === selecionada)
            {
                card.parentElement.style.display = "block";
            }else
            {
                card.parentElement.style.display = "none";
            };
    });
});
})