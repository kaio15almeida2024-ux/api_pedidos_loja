async function buscarGeral() {
    await fetch("http://localhost:5226/api/Cliente")
    .then(Response => Response.json())
    .then(clientes => {
        const listaGeral = document.getElementById("Cliente-Lista")

        clientes.forEach(c => {
            const li = document.createElement("li")
            li.innerHTML = `nome: ${c.nome} - email: ${c.email} 
            <button onclick="editar('${c.idcliente}', '${c.nome}', '${c.email}')">editar</button>
            <button onclick="excluir('${c.idcliente}', '${c.nome}', '${c.email}')">excluir</button>
            
            
            
            
            
            `
            listaGeral.appendChild(li)
        });  
        })

}
buscarGeral()


async function cadastrarcliente() {

    const nome = document.getElementById("nomecliente").value
    const email = document.getElementById("emailcliente").value

    await fetch("http://localhost:5226/api/Cliente", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            nome: nome,
            email: email
        })
    })
    
        .then(response => response.json())
    .then(dados => {
        alert("Cliente cadastrado com sucesso")
        console.log(dados)
    })

      .catch((error) => {
            console.log("Algo deu errado", error)
        })

}

let clienteeditado = null;

async function editar(id,nome,emai) {
        document.getElementById('nomecliente').value = nome
        document.getElementById('emailcliente').value = emai
        
       clienteeditado = id  
}


async function atualizarcliente() {
    
    if(!clienteeditado){
        alert("clique no editar primeiro")
        return
    }

       
       const nome = document.getElementById('nomecliente').value 
       const emai = document.getElementById('emailcliente').value

          await fetch(`http://localhost:5226/api/Cliente/${clienteeditado}`, {
       method:'PUT',
       headers:{
        'Content-Type' : 'application/json'
       },
       body:JSON.stringify({
         nome:nome,
         email:emai,
         pedidos:[]
       })

})

   .catch((error) => {
            console.log("Algo deu errado", error)
        })

   alert("usuario atualizado com sucesso")

}

async function excluir(id){
    const confirmar = confirm("Tem certeza que deseja excluir esse cliente do sistema?")

    if(!confirmar){
       return
    }

     await fetch(`http://localhost:5226/api/Cliente/${id}`,{
           method:'DELETE'

     })
     alert("Pedido excluido com sucesso")

     window.location.reload();
}