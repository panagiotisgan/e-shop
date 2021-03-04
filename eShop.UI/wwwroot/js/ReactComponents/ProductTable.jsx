class Product{
    constructor(productId, productName, productQuantity, imagePath, productPrice, productCode) {
        this.productId = productId;
        this.productPrice = productPrice;
        this.imagePath = imagePath;
        this.productName = productName;
        this.productQuantity = productQuantity;
        this.productCode = productCode;
    }

}

const styles = {
    maxWidth: "80px",
    maxHeight: "80px"
}

class CreateProductTable extends React.Component {
    constructor() {
        super()
        this.state = {
            data: [],
            loading: true,
            images: []
        }
        this.productIds = [];
        this.products = [];
        this.editProduct = this.editProduct.bind(this);
    }


    componentDidMount() {
        fetch("https://localhost:44371/api/products/GetProducts"
            //{
            //    method: "GET",
            //    mode: "cors",
            //    credentials: "same-origin",
            //    headers: {
            //        "Content-Type": "application/json",
            //        "Accept": "application/json",
            //        "Access-Control-Allow-Origin": "*"
            //        //"Access-Control-Allow-Credential": "true"
            //    }
            //redirect: "follow"
            //}
        )
            .then(response => response.json())
            .then(result => {
                this.setState({ data: result, loading: false })
                console.log("there");
                console.log(this.state.data);
                this.state.data.forEach(elem => {
                    this.productIds.push(elem.id)
                });

                console.log("Ids");
                console.log(this.productIds);
                return this.productIds;
            })
            .then(productIds => {
                fetch("https://localhost:44371/api/Images/GetByIds/" + JSON.stringify(productIds), {
                    method: "GET"
                })
                    .then(response => response.json())
                    .then(result => {
                        console.log(result);
                        this.setState({ images: result })
                    })
                    .catch(err => console.log(err))
            }
            )
            .catch(err => console.log(err));


    }

    createTable(data, images) {
        data.forEach(d => {
            images.forEach(img => {
                if (img.productId == d.id) {
                    let prod = new Product(d.id, d.name, d.stockQty, img.imagePath, d.price, d.code);
                    this.products.push(prod);
                }
            })
        })
        console.log(this.products);
    }

    editProduct = id => e => {
        return (<EditProduct productId={id}/>
            )
        console.log("Hello click");
        console.log(id);
        //console.log(id);
        //console.log(e);
    }

    renderTable(data, images) {
        this.createTable(data, images);
        return (
            <table className="table table-striped">
                <thead className="thead-light">
                    <tr>
                        <th></th>
                        <th>Name</th>
                        <th>Price</th>
                        <th>Quantity</th>
                        <th></th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {this.products.map(pr =>
                        <tr key={pr.productId}>
                            <td></td>
                            <td>{pr.productName}</td>
                            <td>{pr.productPrice}</td>
                            <td>{pr.productQuantity}</td>
                            <td><img className="card-columns" style={styles} src={`/images/${pr.imagePath}`} /></td>
                            <th><a onClick={this.editProduct(pr.productId)}>edit</a></th>
                            {/* <th><a onClick={<EditProduct productId={pr.productId}/>}>edit</a></th> */}
                        </tr>
                    )}
                </tbody>
            </table>
        )
    }

    render() {
        let contents = this.state.loading ?
            <p><em>Loading...</em></p> : this.renderTable(this.state.data, this.state.images);

        return (
            <div>
                {contents}
            </div>);
    }
}

ReactDOM.render(<CreateProductTable />, document.getElementById('container'))