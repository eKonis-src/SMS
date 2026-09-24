export interface Person {
    id: number
    firstName: string
    lastName: string
    email: string
    jobTitle: string
    hireDate: string
    isActive: boolean
}

export async function getPeople(): Promise<Person[]> {
    const res = await fetch('/api/people')
    if (!res.ok) throw new Error(`Request failed: ${res.status}`)
    return res.json()
}