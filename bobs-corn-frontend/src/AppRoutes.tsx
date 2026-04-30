import { Route, Routes } from "react-router";
import NotFoundRoute from "./components/NotFoundRoute";
import Home from "./features/home/components/Home";
import CornsPurchased from "./features/cornPurchased/components/CornPurchased";

export default function AppRoutes() {
    return (
        <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/corns-purchased" element={<CornsPurchased />} />
            <Route path="*" element={<NotFoundRoute />} />
        </Routes>
    );
}