import { NavLink } from "react-router";

export default function Menu() {

    return (
        <nav className="navbar navbar-expand-lg navbar-light bg-light border-buttom">
            <div className="container-fluid">
                <NavLink to="/" className="navbar-brand fw-bold">
                    <img
                        src="/assets/corn1.png"
                        alt="Logo"
                        className="brand-logo"
                        />
                    Bob&apos;s Corn
                </NavLink>

                <div className="navbar-nav">
                    <NavLink to="/corns-purchased" className="nav-link">
                        Buy Corn
                    </NavLink>
                </div>
            </div>
        </nav>
    );
}