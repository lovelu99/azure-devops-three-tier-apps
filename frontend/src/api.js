const API_BASE = import.meta.env.VITE_API_BASE || "/api";

export async function getProducts() {
  const response = await fetch(`${API_BASE}/products`);
  if (!response.ok) {
    throw new Error("Failed to fetch products");
  }
  return response.json();
}