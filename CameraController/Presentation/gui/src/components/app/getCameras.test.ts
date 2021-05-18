import { rest } from 'msw'
import { setupServer } from 'msw/node'

import getCameras from "./getCameras";
import { getCamerasUrl } from './urls';

jest.mock('./urls');

const handlers = [
  rest.get('/not-found', (req, res, ctx) => res(ctx.status(404))),
];

const server = setupServer(...handlers);
const getCamerasUrlMock = getCamerasUrl as jest.Mock;

beforeAll(() => server.listen());
afterAll(() => server.close());

it('should throw when given an invalid url', async () => {
  getCamerasUrlMock.mockReturnValue('not-found');
  await expect(getCameras()).rejects.toThrow();
});
