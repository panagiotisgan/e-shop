import React, { Component } from 'react'
import './popup.css';
import Popup from 'reactjs-popup'

class EditProduct extends Component {
    constructor() {
        super()
        this.state = {
            ...this.returnStateObject()
        }
    }


    //componentDidMount() {
    //    fetch("https://localhost:44371/api/products/GetProduct/" + this.props.productId)
    //        .then(response => response.json())
    //        .then(result => {
    //            console.log('result');
    //            console.log(result);
    //            this.setState({ product: result });
    //        }
    //        );

    //}


    //    return (
    //        <div className="modal">
    //            <div className="modal_content">
    //                <span className="close" style={css} />
    //                <form>
    //                    <label>Name</label>
    //                    <input type="text" value={this.props.selectedProduct.name} /><br />
    //                    <label>Price</label>
    //                    <input type="text" value={this.props.selectedProduct.price} /><br />
    //                    <label>Quantity</label>
    //                    <input type="text" value={this.props.selectedProduct.quantity} />
    //                </form>
    //            </div>
    //        </div>
    //    );
    //}
    returnStateObject() {
        if (this.props.currentIndex == -1)
            return {
                productPrice: '',
                productName: '',
                productQuantity: ''
            }
        else
            return this.props.list[this.props.currentIndex]


    }

    componentDidMount(prevProps) {
        if (prevProps.currentIndex != this.props.currentIndex || prevProps.list.length != this.list.length)
            this.setState({ ...this.returnStateObject() })
    }

    handleInputChange = e => {
        this.setState({
            [e.target.name]: e.target.value
        })
    }

    handleSubmit = e => {
        e.preventDefault();
        this.props.onAddOrEdit(this.state);
    }


    render() {
        return (
            <form onSubmit={this.handleSubmit} autoComplete="false">
                <input type="text" name="productName" value={this.state.productName} onChange={this.handleInputChange} placeholder="product name" />
                <input type="text" name="productPrice" value={this.state.productPrice} onChange={this.handleInputChange} placeholder="30.80" />
                <input type="text" name="productQuantity" value={this.state.productQuantity} onChange={this.handleInputChange} placeholder="15" />
                <button type="submit">Submit</button>
            </form>
        );
    }

    /*{this.state.product.name} {this.props.product.Price} {this.props.Quantity}*/
}


export default EditProduct
//ReactDOM.render(<EditProduct />, document.getElementById('popup-container'));