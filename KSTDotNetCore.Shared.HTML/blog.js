const tblBlog = "blogs"
getBlogTable();
let blogId = null;
// testConfirmMessage();
// createBlog();
// deleteBlog("103e867e-f95c-4316-a935-c6db55e0fba6");
// updateBlog("efbbebb2-dce0-4a31-8797-125e457206bf", "a", "b", "C");

function testConfirmMessage() {
    let confirmMessage = new Promise(function (success, error) {
        const result = confirm("are you sure to delete")
        if (result) {
            success();
        } else {
            error();
        }
    });
    confirmMessage.then(
        function (value) {
            successMessage("Success");
        },
        function (error) {
            errorMessage("Error")
        }
    )
}

function testConfirmMessage2() {
    let confirmMessage = new Promise(function (success, error) {
        Swal.fire({
            title: "Confirm",
            text: message,
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes"
        }).then((result) => {
            if (result.isConfirmed) {
                success();
            } else {
                error();
            };
        }
        )
    });
};

function readBlog() {
    const blogs = localStorage.getItem(tblBlog);
    console.log(blogs)
}

function editBlog(id) {
    let lst = getBlogs();
    const items = lst.filter(x => x.id === id)
    console.log(items)
    console.log(items.length)

    if (items.length === 0) {
        console.log("no data found");
        errorMessage("no data found");
        return;
    }

    let item = items[0]

    blogId = item.id;
    $('#txtTitle').val(item.title);
    $('#txtAuthor').val(item.author);
    $('#txtContent').val(item.content);
    $('#txtTitle').focus();

    // return items[0]
}

function createBlog(title, author, content) {
    let lst = getBlogs();

    const requestModel = {
        id: uuidv4(),
        title: title,
        author: author,
        content: content
    };

    lst.push(requestModel)
    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog)

    successMessage("Saving success.")
    clearControls()

    // const jsonBlog = JSON.stringify(requestModel);
    // localStorage.setItem(tblBlog, jsonBlog)
}

function updateBlog(id, title, author, content) {
    let lst = getBlogs();

    const items = lst.filter(x => x.id === id)
    console.log("ITEMS : " + JSON.stringify(items))

    console.log(items.length)

    if (items.length === 0) {
        console.log("No data found");
        return;
    }

    const item = items[0];
    item.title = title;
    item.author = author;
    item.content = content;

    console.log("ITEM : " + JSON.stringify(item))

    const index = lst.findIndex(x => x.id === id)
    lst[index] = item;

    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog)

    successMessage("updating successful");
}

function deleteBlog2(id) {
    let result = confirm("Are you sure to delete");
    if (!result) return;

    let lst = getBlogs();

    const items = lst.filter(x => x.id === id);
    if (items.length == 0) {
        console.log("no data found");
        return;
    }

    lst = lst.filter(x => x.id !== id);
    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog);

    successMessage("Deleting successful");

    getBlogTable();
}

function deleteBlog1(id) {

    Swal.fire({
        title: "Confirm",
        text: "Are you sure to delete!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes"
    }).then((result) => {
        if (!result.isConfirmed) return;

        let lst = getBlogs();

        const items = lst.filter(x => x.id === id);
        if (items.length == 0) {
            console.log("no data found");
            return;
        }

        lst = lst.filter(x => x.id !== id);
        const jsonBlog = JSON.stringify(lst);
        localStorage.setItem(tblBlog, jsonBlog);

        successMessage("Deleting successful");

        getBlogTable();

        Swal.fire({
            title: "Deleted!",
            text: "Your file has been deleted.",
            icon: "success"
        });

    });
}

function deleteBlog(id) {
    confirmMessage("Are you sure to delete").then(
        function (value) {
            let lst = getBlogs();

            const items = lst.filter(x => x.id === id);
            if (items.length == 0) {
                console.log("no data found");
                return;
            }

            lst = lst.filter(x => x.id !== id);
            const jsonBlog = JSON.stringify(lst);
            localStorage.setItem(tblBlog, jsonBlog);

            successMessage("Deleting successful");

            getBlogTable();
        }
    )


}


function getBlogs() {
    const blogs = localStorage.getItem(tblBlog);
    console.log(blogs)

    let lst = []
    if (blogs !== null) {
        lst = JSON.parse(blogs);
    }
    return lst;
}

$('#btnSave').click(function () {
    const title = $('#txtTitle').val();
    const author = $('#txtAuthor').val();
    const content = $('#txtContent').val();

    if (blogId === null) {
        createBlog(title, author, content);
    } else {
        updateBlog(blogId, title, author, content);
        blogId = null;
    }

    getBlogTable();
})



let clearControls = () => {
    $('#txtTitle').val('');
    $('#txtAuthor').val('');
    $('#txtContent').val('');
    $('#txtTitle').focus();
}

function getBlogTable() {
    const lst = getBlogs();
    let count = 0;
    let htmlRows = '';
    lst.forEach(item => {
        const htmlRow = `
        <tr>
            <td>
                <button type="button" class="btn btn-warning" onclick="editBlog('${item.id}')" >Edit</button>
                <button type="button" class="btn btn-danger" onclick="deleteBlog('${item.id}')" >Delete</button>
            </td>
            <td>${++count}</td>
            <td>${item.title}</td>
            <td>${item.author}</td>
            <td>${item.content}</td>
        </tr>`;
        htmlRows += htmlRow;
    });

    $('#tbody').html(htmlRows);
}

