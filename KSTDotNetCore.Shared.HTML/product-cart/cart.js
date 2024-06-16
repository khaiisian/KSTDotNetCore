let Product = "tblProduct";
let Cart = "tblCartProduct";
getProductTbl();
getCartTbl();
let productId = null;

function createProduct(name, price, desc) {
    let lst = getProducts();
    if (name == "" || price == "" || desc == "") {
        errorMessage("No data found to save.")
        return;
    }

    const requestModel = {
        id: uuidv4(),
        name: name,
        price: price,
        description: desc
    };


    lst.push(requestModel);
    const jsonProduct = JSON.stringify(lst);
    localStorage.setItem(Product, jsonProduct);

    successMessage("Product is saved successfully.");
    clearControls();
}

function editProduct(id) {
    let lst = getProducts();
    let items = lst.filter(x => x.id === id);
    if (items.length == 0) {
        errorMessage("No data found");
        return;
    }

    let item = items[0]
    productId = item.id;
    $('#txtName').val(item.name);
    $('#txtPrice').val(item.price);
    $('#txtDesc').val(item.description);
    $('#txtName').focus();
}

function confirmUpdate(id, name, price, desc) {
    confirm("Are you sure to update the product", updateProduct, id, name, price, desc)
}
function updateProduct(id, name, price, desc) {
    let lst = getProducts();

    let items = lst.filter(x => x.id === id)
    if (items.length == 0) {
        errorMessage("No data found.");
        return;
    }

    const item = items[0];
    item.name = name;
    item.price = price;
    item.description = desc;

    const index = lst.findIndex(x => x.id === id);
    lst[index] = item;

    const jsonProduct = JSON.stringify(lst);
    localStorage.setItem(Product, jsonProduct);

    successMessage("Successfully Updated.");
    getProductTbl();
}

function confirmDelete(id) {
    confirm("Are you sure to delete the product", deleteProduct, id)
}

function deleteProduct(id) {
    let lst = getProducts();

    const items = lst.filter(x => x.id === id);
    if (items.length == 0) {
        console.log("No data found to delete");
        return;
    }

    lst = lst.filter(x => x.id != id);
    const jsonProduct = JSON.stringify(lst);
    localStorage.setItem(Product, jsonProduct);

    getProductTbl();
}

function getProducts() {
    const products = localStorage.getItem(Product);
    let lst = []
    if (products !== null) {
        lst = JSON.parse(products)
    }
    return lst;
}

function uuidv4() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
        (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
    );
}

// For Cart
function createCart(id) {
    let products = getProducts();

    let items = products.filter(x => x.id === id);
    if (items.length == 0) {
        errorMessage("No Data Found")
        return;
    }
    let item = items[0];

    let clst = getCartItem();
    let cItems = clst.filter(x => x.id === id);
    if (cItems.length !== 0) {
        let cItem = cItems[0]
        console.log(cItem.id)
        if (cItem.id === id) {
            cItem.quantity = ++cItem.quantity;
            const index = clst.findIndex(x => x.id == id);
            clst[index] = cItem;
            const jsonCartProduct = JSON.stringify(clst);
            localStorage.setItem(Cart, jsonCartProduct);
            successMessage("Already added to the cart");
            getCartTbl();
            return;
        }
    }

    const requestModel = {
        id: id,
        name: item.name,
        price: item.price,
        quantity: 1,
        desc: item.description,
    }
    clst.push(requestModel);
    const jsonCartProduct = JSON.stringify(clst);
    localStorage.setItem(Cart, jsonCartProduct);
    successMessage("Successfully added to the cart");
    getCartTbl();
}

function removeCart(id) {
    let clst = getCartItem();
    const cItems = clst.filter(x => x.id === id);
    if (cItems.length === 0) {
        errorMessage("No data found to remove");
        return;
    }
    clst = clst.filter(x => x.id !== id);
    const jsonCartProduct = JSON.stringify(clst);
    localStorage.setItem(Cart, jsonCartProduct);
    successMessage("Successfully remove the cart");
    getCartTbl();
}

function addCart(id) {
    let clst = getCartItem();
    let cItems = clst.filter(x => x.id === id)
    let cItem = cItems[0];
    cItem.quantity = ++cItem.quantity;
    const index = clst.findIndex(x => x.id == id);
    clst[index] = cItem;
    const jsonCartProduct = JSON.stringify(clst);
    localStorage.setItem(Cart, jsonCartProduct);
    getCartTbl();
    return;
}

function redCart(id) {
    let clst = getCartItem();
    let cItems = clst.filter(x => x.id === id)
    let cItem = cItems[0];
    if (cItem.quantity === 0) {
        return;
    }
    cItem.quantity = --cItem.quantity;
    const index = clst.findIndex(x => x.id == id);
    clst[index] = cItem;
    const jsonCartProduct = JSON.stringify(clst);
    localStorage.setItem(Cart, jsonCartProduct);
    getCartTbl();
    return;
}

function getCartItem() {
    const cartProducts = localStorage.getItem(Cart);
    let lst = []
    if (cartProducts !== null) {
        lst = JSON.parse(cartProducts)
    }
    return lst;
}

$('#btnSave').click(function () {
    const name = $('#txtName').val();
    const price = $('#txtPrice').val();
    const desc = $('#txtDesc').val();

    if (productId == null) {
        createProduct(name, price, desc);
    } else {
        confirmUpdate(productId, name, price, desc)
        // updateProduct(productId, name, price, desc)
    }

    getProductTbl();
})

$('#showCartBtn').click(function () {
    toggleCart();
})

$('#close_cart').click(function () {
    toggleCart()
})

function toggleCart() {

    let cTbl = document.getElementById('_cTbl');
    let overlay = document.getElementById('_overlay')
    cTbl.classList.toggle('show');
    overlay.classList.toggle("overlay");
}

function successMessage(message) {
    Notiflix.Report.success(
        'Success',
        message,
        'Okay',
    );
}

function errorMessage(message) {
    Notiflix.Report.failure(
        'Failure',
        message,
        'Okay',
    );
}

function confirm(message, func, ...para) {
    Notiflix.Confirm.show(
        'Confirm',
        message,
        'Yes',
        'No',
        function okCb() {
            func(...para);
        },
        function cancelCb() {
            Notiflix.Report.info(
                'Cancel',
                'Process Canceled',
                'Okay',
            );;
        }
    );
}

function clearControls() {
    $('#txtName').val('');
    $('#txtPrice').val('');
    $('#txtDesc').val('');
    $('#txtName').focus();
}

function getProductTbl() {
    const lst = getProducts();
    let count = 0;
    let rows = '';
    lst.forEach(item => {
        console.log(item)
        const row = `
        <tr>
            <th scope="row">${++count}.</th>
            <td>
                <button type="button" class="btn btn-warning" onclick="editProduct('${item.id}')" >Edit</button>  
                <button type="button" class="btn btn-danger" onclick="confirmDelete('${item.id}')">Delete</button>          
            </td>
            <td>${item.name}</td>
            <td>${item.price}</td>
            <td>${item.description}</td>
            <td>
                <button type="button" class="btn btn-primary" onclick= "createCart('${item.id}')">Add To Cart</button>
            </td>
        </tr>
        `;
        rows += row;
    });
    $('#tbody').html(rows);
}

function getCartTbl() {
    let cartLst = getCartItem();
    let count = 0;
    let crows = '';
    cartLst.forEach(item => {
        const crow = `
        <tr>
            <td>${++count}</td>
            <td>${item.name}</td>
            <td>${item.desc}</td>
            <td class = "qty">
            <center>
            <button type="button" class="btn btn-secondary" onclick = "addCart('${item.id}')"><i class="fa-solid fa-plus"></i></button>
            <span>${item.quantity}</span>
            <button type="button" class="btn btn-secondary" onclick = "redCart('${item.id}')"><i class="fa-solid fa-minus"></i></button></center></td>
            <td>${item.quantity * item.price}</td>
            <td><button type="button" class="btn btn-danger" onclick = "removeCart('${item.id}')">Remove</button></td>
        </tr>
        `;
        crows += crow;
    });
    $('#cBody').html(crows);
}