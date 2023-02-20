import faker, { name } from 'faker';

faker.seed(1337);

function genUsers(num = 1000) {
  faker.seed(1337);
  let result = [];

  for (let i = 0; i < num; i++) {
    let nev = name.firstName() + ' ' + name.lastName();

    const row = { id: i, text: nev };
    result.push(row);
  }
  return result;
}
export const mockUsers = genUsers(250);
