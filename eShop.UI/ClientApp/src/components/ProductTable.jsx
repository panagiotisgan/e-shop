import React, { Component, useEffect, useState } from 'react'
import EditProduct from './EditPopUp'
import Popup from 'reactjs-popup';
import { Button } from 'react-bootstrap';
import { render } from 'react-dom';

class Product {
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

class CreateProductTable extends Component {
    constructor() {
        super()
        this.state = {
            list: [],
            loading: true,
            images: [],
            visible: false,
            currentIndex: -1
        };
        this.productIds = [];
        this.products = [];
        this.externalList = [];
        //this.editProduct = this.editProduct.bind(this);

        this.handleEdit = this.handleEdit.bind(this);
        this.onAddOrEdit = this.onAddOrEdit.bind(this);
    }

    onAddOrEdit = (data) => {
        var list = this.externalList.push(data);
        this.setState({ list: list });
    }

    //const [list, setData] = useState([]);
    //const [loading, setLoading] = useState(true);
    //const [images, setImages] = useState([]);

    //var productIds = [];
    //var products = [];


    //useEffect(() => {
    //    fetch("https://localhost:44371/api/products/GetProducts")
    //        .then(response => response.json())
    //        .then((result) => {
    //            console.log(result);                
    //            setData([...list, result]);
    //            console.log(list);
    //            setLoading(false);
    //            console.log("there");
    //            console.log(list);
    //            list.forEach(elem => {
    //                productIds.push(elem.id)
    //            });

    //            console.log("Ids");
    //            console.log(productIds);
    //            return productIds;
    //        })
    //        .then(productIds => {
    //            fetch("https://localhost:44371/api/Images/GetByIds/" + JSON.stringify(productIds), {
    //                method: "GET"
    //            })
    //                .then(response => response.json())
    //                .then(result => {
    //                    console.log(result);
    //                    setImages(images => [...images, result]);

    //                })
    //                .catch(err => console.log(err))
    //        }
    //        )
    //        .catch(err => console.log(err));
    //},[])

    /*COMPONENT DID MOUNT IMPLEMENTATION IN CLASS*/
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
                this.setState({ list: result, loading: false })
                result.forEach(prod => {
                    this.externalList.push(prod);
                })
                console.log("there");
                console.log(this.state.list);
                console.log("external");
                console.log(this.externalList);
                this.state.list.forEach(elem => {
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
                        this.setState({ images: result });
                    })
                    .catch(err => console.log(err))
            }
            )
            .catch(err => console.log(err));
    }


    createTable(list, images) {
        list.forEach(d => {
            images.forEach(img => {
                if (img.productId === d.id) {
                    let prod = new Product(d.id, d.name, d.stockQty, img.imagePath, d.price, d.code);
                    this.products.push(prod);
                }
            })
        })
        console.log(this.products);
    }

    //editProduct = (productId) => {
    //    {/*return (<EditProduct productId={id}/>)*/ }
    //    var product;
    //    console.log("Hello click");
    //    console.log(productId);
    //    fetch("https://localhost:44371/api/Products/GetProduct/" + JSON.stringify(productId),
    //        {
    //            method: 'GET'
    //        })
    //        .then(response => response.json())
    //        .then(result => {
    //            product = result;
    //            //console.log(product);
    //            //Gia na perasw ta list sto epomeno then pou 8a kalei to popup edit product
    //            return product;
    //        })
    //        .then(product => {
    //            console.log(product);                
    //        })
    //        .catch(err => console.log(err));
    //}

    handleEdit = index => {
        this.setState({
            currentIndex: index
        });
    }

    renderTable(list, images) {
        this.createTable(list, images);
        return <table className="table table-striped">
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
                {this.products.map((pr,index) =>
                    <tr key={index}>
                        <td></td>
                        <td>{pr.productName}</td>
                        <td>{pr.productPrice}</td>
                        <td>{pr.productQuantity}</td>
                        <td><img className="card-columns" style={styles} src={`/images/${pr.imagePath}`} /></td>
                        <td><button onClick={() => this.handleEdit(index)}>Edit</button></td>
                    </tr>
                )}
            </tbody>
        </table>
    }

    render() {
        let contents = this.state.loading ? <p><em>Loading...</em></p> : this.renderTable(this.state.list, this.state.images);

        return (
            <div>
                <EditProduct
                    onAddOrEdit={this.onAddOrEdit}
                    currentIndex={this.state.currentIndex}
                    list={this.state.list} />
                <div>
                    {contents}
                </div>
            </div>);
    }
}

export default CreateProductTable
//ReactDOM.render(<CreateProductTable />, document.getElementById('container'))