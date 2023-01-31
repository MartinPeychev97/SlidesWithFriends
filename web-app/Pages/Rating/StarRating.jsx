import React from 'react';
import ReactDOM from 'react-dom';
import './star-rating.module.css';

class StarRating extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            rating: 0
        };
        this.updateRating = this.updateRating.bind(this);
    }

    updateRating(newRating) {
        this.setState({ rating: newRating });
    }

    render() {
        const stars = [1, 2, 3, 4, 5];
        return (
            <div>
                {stars.map(star => (
                    <span key={star} onClick={() => this.updateRating(star)}>
                        {'\u2605'}
                    </span>
                ))}
                <p>Current rating: {this.state.rating}</p>
            </div>
        );
    }
}

return (  <div id="star-rating"> TEST</div>);

ReactDOM.render(<StarRating />, document.getElementById('star-rating'));

export default StarRating;
