const apiUrl = 'https://localhost:7032/api/Biblioteca';

// Função para listar livros
async function fetchBooks() {
    const response = await fetch(apiUrl);
    const books = await response.json();
    const bookList = document.getElementById('bookList');
    bookList.innerHTML = '';
    books.forEach(book => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
            <td>${book.titulo}</td>
            <td>${book.autor}</td>
            <td>${book.paginas}</td>
            <td>R$${book.preco.toFixed(2)}</td>
            <td>
                <button class="edit-btn" onclick="editBook(${book.id})">Editar</button>
                <button class="delete-btn" onclick="deleteBook(${book.id})">Excluir</button>
            </td>
        `;
        bookList.appendChild(tr);
    });
}

// Função para adicionar um novo livro
document.getElementById('addBookForm').addEventListener('submit', async (e) => {
    e.preventDefault();
    const titulo = document.getElementById('titulo').value;
    const autor = document.getElementById('autor').value;
    const paginas = document.getElementById('paginas').value;
    const preco = document.getElementById('preco').value;

    const newBook = {
        titulo,
        autor,
        paginas,
        preco
    };

    await fetch(apiUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newBook)
    });

    fetchBooks();
});

// Função para excluir um livro
async function deleteBook(id) {
    if (confirm('Tem certeza que deseja excluir este livro?')) {
        await fetch(`${apiUrl}/${id}`, {
            method: 'DELETE'
        });

        fetchBooks();
    }
}

// Função para editar um livro
async function editBook(id) {
    const livro = await fetch(`${apiUrl}/${id}`).then(res => res.json());

    const newTitulo = prompt('Novo Título:', livro.titulo);
    const newAutor = prompt('Novo Autor:', livro.autor);
    const newPaginas = prompt('Novas Páginas:', livro.paginas);
    const newPreco = prompt('Novo Preço:', livro.preco);

    const updatedBook = {
        titulo: newTitulo,
        autor: newAutor,
        paginas: parseInt(newPaginas),
        preco: parseFloat(newPreco)
    };

    await fetch(`${apiUrl}/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(updatedBook)
    });

    fetchBooks();
}

// Inicializa a lista de livros ao carregar a página
window.onload = fetchBooks;
