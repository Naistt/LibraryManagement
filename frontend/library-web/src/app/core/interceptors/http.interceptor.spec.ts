import { httpInterceptor } from './http.interceptor';

describe('httpInterceptor', () => {
  it('should be defined', () => {
    expect(httpInterceptor).toBeDefined();
    expect(typeof httpInterceptor).toBe('function');
  });
});
