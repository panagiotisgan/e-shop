class EditProduct extends React.Component {
    constructor(props) {
        super()
        this.state = {
            productId: 0,
            product: null
        };
    }

    componentDidMount() {
        fetch("https://localhost:44371/api/products/GetProduct/" + this.props.productId)
            .then(response => response.json())
            .then(result => {
                console.log('result');
                console.log(result);
                this.setState({ product: result });
            }
            );

    }

    render() {
        return (
            <div>
                <form>
                    <label>Name</label>
                    <input type="text" value={this.state.product.name} /><br/>
                    <label>Price</label>
                    <input type="text" value={this.props.product.Price} /><br />
                    <label>Quantity</label>
                    <input type="text" value={this.props.Quantity}/>
                </form>
            </div>
        );
    }
}

ReactDOM.render(<EditProduct />, document.getElementById('popup-container'));