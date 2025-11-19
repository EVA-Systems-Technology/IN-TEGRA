document.addEventListener("DOMContentLoaded", function() {
    const inputPesquisa = document.getElementById('campoPesquisa');
    const listaResultados = document.getElementById('listaResultados');

    inputPesquisa.addEventListener('keyup', async function() {
    
        const searchTerm = inputPesquisa.value;

        if (searchTerm.length < 2) {
            listaResultados.innerHTML = '';
            listaResultados.style.display = 'none';
            return;
        }
        
        fetch(`/Produto/Pesquisar?searchTerm=${searchTerm}`)
            .then(response => response.json())
            .then(data => {
                listaResultados.innerHTML = '';
                if (data.length === 0) {
                    listaResultados.style.display = 'none';
                } else {
                    listaResultados.style.display = 'block';
                    
                    data.forEach(prod => {
                        const link = document.createElement('a');
                        link.href = `/Produto/DetalhesProduto?IdProd=${prod.id}`;
                        link.className = 'item-resultado';

                        link.innerHTML = `
                                <span class="item-nome">${prod.nome}</span>
                                <span class="item-preco">${prod.preco}</span>
                            `;

                        listaResultados.appendChild(link);
                    });
                }
            })
            .catch(error => console.error('Erro na pesquisa:', error));
    });
    
    document.addEventListener('click', function(e) {
        if (!inputPesquisa.contains(e.target) && !listaResultados.contains(e.target)) {
            listaResultados.style.display = 'none';
        }
    });
});